using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using Kiva.FPS.Lib.Contracts;

namespace Kiva.FPS.Lib.API;

public class Endpoint
{
    #region public events/properties
    public string Token { get => token; }
    public event EndpointLogMessage OnLogMessage;
    #endregion

    #region private data
    private readonly Credentials credentials;
    private readonly int partnerId;
    private string token = string.Empty;
    private DateTimeOffset tokenExpiry = DateTimeOffset.MinValue;

    private record Auth0TokenResponse(
        [property: JsonPropertyName("expires_in")] int ExpiresIn);
    #endregion

    #region constructor
    /// <summary>
    /// Creates an Endpoint instance configured for the given credentials and partner.
    /// </summary>
    /// <param name="credentials">Kiva API credentials including audience and scopes.</param>
    /// <param name="partnerId">Your Kiva Partner ID.</param>
    public Endpoint(Credentials credentials, int partnerId)
    {
        this.credentials = credentials;
        this.partnerId = partnerId;
    }
    #endregion

    #region public methods
    /// <summary>
    /// Authenticates with the Kiva API and caches the Bearer token.
    /// Safe to call multiple times — skips the network call if the token is still valid.
    /// see https://fps-sdk-portal.web.app/docs/overview/authentication
    /// </summary>
    /// <exception cref="KivaDefaultException">thrown on error, see message for more details</exception>
    public void Login()
    {
        // ---------------------------------------------------------------------------
        // return early if the token is still valid
        if (!string.IsNullOrEmpty(token) && DateTimeOffset.UtcNow < tokenExpiry)
        {
            LogMessage("Login skipped: existing token is still valid");
            return;
        }

        // ---------------------------------------------------------------------------
        // create the http client
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();

        // ---------------------------------------------------------------------------
        // set accepted type to json
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var parameters = new Dictionary<string, string>
        {
            { "client_id", credentials.ClientId },
            { "client_secret", credentials.ClientSecret },
            { "grant_type", "client_credentials" },
            { "audience", credentials.Audience.ToAudienceString() },
            { "scope", credentials.Scopes.ToScopeString() }
        };

        // ---------------------------------------------------------------------------
        // since the API expects the details to be posted as x-www-form-urlencoded
        // we have to properly encode each value
        var encodedContent = new FormUrlEncodedContent(parameters);

        // ---------------------------------------------------------------------------
        // make the call
        string domain = $"https://{GetAuthServer(credentials.Audience)}/oauth/token";
        var response = client.PostAsync(domain, encodedContent).Result;

        // ---------------------------------------------------------------------------
        // process the response
        if (response.StatusCode != HttpStatusCode.OK)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            LogMessage($"Login error: {response.StatusCode}: {result}");
            throw new KivaDefaultException($"Login error: {response.StatusCode}: {result}");
        }

        token = response.Content.ReadAsStringAsync().Result;

        // ---------------------------------------------------------------------------
        // parse expires_in to cache the token expiry (with a 60-second safety buffer)
        var tokenData = JsonSerializer.Deserialize<Auth0TokenResponse>(token);
        tokenExpiry = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(tokenData!.ExpiresIn - 60);

        LogMessage("Login complete");
    }

    /// <summary>
    /// Returns the list of activities available for the partner.
    /// </summary>
    public LoanActivitiesResponse GetActivities()
    {
        Login();
        return Get<LoanActivitiesResponse>($"/v3/partner/{partnerId}/config/activities");
    }

    /// <summary>
    /// Returns the list of loan themes available for the partner.
    /// </summary>
    public LoanThemesResponse GetThemes()
    {
        Login();
        return Get<LoanThemesResponse>($"/v3/partner/{partnerId}/config/themes");
    }

    /// <summary>
    /// Returns the list of locales available for the partner.
    /// </summary>
    public PartnerLocalesResponse GetLocales()
    {
        Login();
        return Get<PartnerLocalesResponse>($"/v3/partner/{partnerId}/config/locales");
    }

    /// <summary>
    /// Returns the list of locations available for the partner.
    /// </summary>
    public PartnerLocationsResponse GetLocations()
    {
        Login();
        return Get<PartnerLocationsResponse>($"/v3/partner/{partnerId}/config/locations");
    }

    /// <summary>
    /// Returns loans for the partner, optionally filtered by the provided request parameters.
    /// </summary>
    public LoanResponse GetLoans(GetLoansRequest? request = null)
    {
        Login();
        var query = new Dictionary<string, string>();
        if (request?.Query != null)   query["query"]  = request.Query;
        if (request?.Status != null)  query["status"] = request.Status;
        if (request?.Offset != null)  query["offset"] = request.Offset;
        if (request?.Limit != null)   query["limit"]  = request.Limit;

        var path = $"/v3/partner/{partnerId}/loans";
        if (query.Count > 0)
            path += "?" + string.Join("&", query.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

        return Get<LoanResponse>(path);
    }

    /// <summary>
    /// Submits repayment records for the partner.
    /// </summary>
    public List<RepaymentResponse> CreateRepayments(RepaymentRequest request)
    {
        Login();
        return Post<RepaymentRequest, List<RepaymentResponse>>($"/v3/partner/{partnerId}/repayments", request);
    }

    /// <summary>
    /// Submits a loan draft for the partner.
    /// </summary>
    public LoanDraftResponse CreateLoanDraft(LoanDraftRequest request)
    {
        Login();
        return Post<LoanDraftRequest, LoanDraftResponse>($"/v3/partner/{partnerId}/loan_draft", request);
    }

    /// <summary>
    /// Submits journal updates for the partner.
    /// </summary>
    public List<JournalResponse> CreateJournals(JournalRequest request)
    {
        Login();
        return Post<JournalRequest, List<JournalResponse>>($"/v3/partner/{partnerId}/journals", request);
    }
    #endregion

    #region private methods
    private TResponse Post<TRequest, TResponse>(string path, TRequest body)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        string url = $"{GetApiBaseUrl(credentials.Audience)}{path}";
        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = client.PostAsync(url, content).Result;

        if (response.StatusCode != HttpStatusCode.OK)
        {
            var responseBody = response.Content.ReadAsStringAsync().Result;
            LogMessage($"POST {path} error: {response.StatusCode}: {responseBody}");
            throw new KivaDefaultException($"POST {path} error: {response.StatusCode}: {responseBody}");
        }

        var responseJson = response.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<TResponse>(responseJson)!;
    }

    private T Get<T>(string path)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        string url = $"{GetApiBaseUrl(credentials.Audience)}{path}";
        var response = client.GetAsync(url).Result;

        if (response.StatusCode != HttpStatusCode.OK)
        {
            var body = response.Content.ReadAsStringAsync().Result;
            LogMessage($"GET {path} error: {response.StatusCode}: {body}");
            throw new KivaDefaultException($"GET {path} error: {response.StatusCode}: {body}");
        }

        var json = response.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<T>(json)!;
    }

    private string GetAuthServer(Audience audience) => audience switch
    {
        Audience.Production => "auth.dk1.kiva.org",
        Audience.Staging => "auth-stage.dk1.kiva.org",
        _ => throw new ArgumentOutOfRangeException(nameof(audience), audience, null)
    };

    private string GetApiBaseUrl(Audience audience) => audience switch
    {
        Audience.Production => "https://partnerapi.kiva.org",
        Audience.Staging => "https://partnerapi.staging.kiva.org",
        _ => throw new ArgumentOutOfRangeException(nameof(audience), audience, null)
    };

    private void LogMessage(string message)
    {
        OnLogMessage?.Invoke(message);
    }
    #endregion
}