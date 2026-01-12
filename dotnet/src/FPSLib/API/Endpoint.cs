using System.Net;
using System.Net.Http.Headers;
using Kiva.FPS.Lib.Contracts;

namespace Kiva.FPS.Lib.API;

/// <summary>
/// 
/// </summary>
public class Endpoint
{
    #region public events/properties
    public string Token {get => token;}
    public event EndpointLogMessage OnLogMessage;
    #endregion
    
    #region private data
    private string token = string.Empty;
    #endregion
    
    #region public properties
    /// <summary>
    /// calls the appropriate login endpoint based on the Credentials.Audience
    /// see https://fps-sdk-portal.web.app/docs/overview/authentication
    /// </summary>
    /// <param name="credentials"></param>
    /// <returns>t</returns>
    /// <exception cref="KivaDefaultException">thrown on error, see message for more details</exception>
    public void Login(Credentials credentials)
    {
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
        string domain = $"https://{GetAuthOServer(credentials.Audience)}/oauth/token";
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
        LogMessage("Login complete");
    }
    #endregion

    #region private methods 
    private string GetAuthOServer(Audience audience)
    {
        // TODO we should probably move this to a config file
        return audience switch
        {
            Audience.Production => "auth.dk1.kiva.org",
            Audience.Staging => "auth-stage.dk1.kiva.org", 
            _ => throw new ArgumentOutOfRangeException(nameof(audience), audience, null)
        };
    }
    
    private void LogMessage(string message)
    {
        OnLogMessage?.Invoke(message);
    }
    #endregion
}