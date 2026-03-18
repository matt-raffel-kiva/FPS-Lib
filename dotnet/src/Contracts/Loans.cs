using System.Text.Json.Serialization;
namespace Kiva.FPS.Lib.Contracts;

// ---------- Loans / Query ----------
public sealed class GetLoansRequest
{
    /// <summary>Custom query using PA2 search bar format.</summary>
    public string? Query { get; set; }

    /// <summary>
    /// Filter by loan status.
    /// Valid values: deleted, issue, issue_revising, issue_approving, reviewed,
    /// fundRaising, refunded, raised, payingBack, ended, defaulted, expired, inactive_expired
    /// </summary>
    public string? Status { get; set; }

    /// <summary>0-indexed offset for pagination.</summary>
    public string? Offset { get; set; }

    /// <summary>Number of records to return.</summary>
    public string? Limit { get; set; }
}

// ---------- Loans / Config ----------
public sealed class Loan
{
    [JsonPropertyName("kiva_id")]
    public string KivaId { get; set; } = default!;

    [JsonPropertyName("borrower_count")]
    public int BorrowerCount { get; set; }

    [JsonPropertyName("internal_loan_id")]
    public string InternalLoanId { get; set; } = default!;

    [JsonPropertyName("internal_client_id")]
    public string InternalClientId { get; set; } = default!;

    [JsonPropertyName("partner_id")]
    public string PartnerId { get; set; } = default!;

    [JsonPropertyName("partner")]
    public string Partner { get; set; } = default!;

    [JsonPropertyName("uuid")]
    public string Uuid { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("location")]
    public string Location { get; set; } = default!;

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("status_detail")]
    public string StatusDetail { get; set; } = default!;

    [JsonPropertyName("loan_price")]
    public string LoanPrice { get; set; } = default!;

    [JsonPropertyName("loan_local_price")]
    public string LoanLocalPrice { get; set; } = default!;

    [JsonPropertyName("loan_currency")]
    public string LoanCurrency { get; set; } = default!;

    [JsonPropertyName("create_time")]
    public long CreateTime { get; set; }

    [JsonPropertyName("planned_expiration_time")]
    public long PlannedExpirationTime { get; set; }

    [JsonPropertyName("planned_inactive_expire_time")]
    public long PlannedInactiveExpireTime { get; set; }

    [JsonPropertyName("delinquent")]
    public bool Delinquent { get; set; }

    // Optional timestamps / fields shown in schema
    [JsonPropertyName("ended_time")] public long? EndedTime { get; set; }
    [JsonPropertyName("refunded_time")] public long? RefundedTime { get; set; }
    [JsonPropertyName("expired_time")] public long? ExpiredTime { get; set; }
    [JsonPropertyName("defaulted_time")] public long? DefaultedTime { get; set; }
    [JsonPropertyName("issue_feedback_time")] public long? IssueFeedbackTime { get; set; }
    [JsonPropertyName("issue_reported_by")] public string? IssueReportedBy { get; set; }
}

public sealed class LoanResponse
{
    [JsonPropertyName("totalRecords")]
    public int TotalRecords { get; set; }

    [JsonPropertyName("data")]
    public List<Loan> Data { get; set; } = new();
}

public sealed class Theme
{
    [JsonPropertyName("themeType")]
    public string ThemeType { get; set; } = default!;

    [JsonPropertyName("themeTypeId")]
    public int ThemeTypeId { get; set; }
}

public sealed class LoanThemesResponse
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [JsonPropertyName("asOfDateTime")]
    public string AsOfDateTime { get; set; } = default!;

    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;

    [JsonPropertyName("themes")]
    public List<Theme> Themes { get; set; } = new();
}

public sealed class Location
{
    [JsonPropertyName("country")]
    public string Country { get; set; } = default!;

    [JsonPropertyName("location")]
    public string LocationName { get; set; } = default!;

    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = default!;
}

public sealed class PartnerLocationsResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;

    [JsonPropertyName("asOfDateTime")]
    public string AsOfDateTime { get; set; } = default!;

    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [JsonPropertyName("locations")]
    public List<Location> Locations { get; set; } = new();
}

public sealed class PartnerLocale
{
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;

    [JsonPropertyName("languageCode")]
    public string LanguageCode { get; set; } = default!;

    [JsonPropertyName("languageName")]
    public string LanguageName { get; set; } = default!;

    [JsonPropertyName("languageId")]
    public int LanguageId { get; set; }
}

public sealed class PartnerLocalesResponse
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [JsonPropertyName("asOfDateTime")]
    public string AsOfDateTime { get; set; } = default!;

    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;

    [JsonPropertyName("locales")]
    public List<PartnerLocale> Locales { get; set; } = new();
}

public sealed class Activity
{
    [JsonPropertyName("activityId")]
    public int ActivityId { get; set; }

    [JsonPropertyName("activityName")]
    public string ActivityName { get; set; } = default!;
}

public sealed class LoanActivitiesResponse
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [JsonPropertyName("asOfDateTime")]
    public string AsOfDateTime { get; set; } = default!;

    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;

    [JsonPropertyName("activities")]
    public List<Activity> Activities { get; set; } = new();
}