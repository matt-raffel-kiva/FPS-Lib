using System.Text.Json.Serialization;

namespace Kiva.FPS.Lib.Contracts;


// TODO:  
// 1 - replace string Dates with DatesTimes
// 2 - replace long timestamps with DateTimes
// 3 - replace string types with references to look up type (eg Theme, Country etc)
// 4 - break out into separate files



// ---------- Repayments ----------
public sealed class Repayment
{
    [JsonPropertyName("loan_id")]
    public string LoanId { get; set; } = default!;

    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}

public sealed class RepaymentRequest
{
    [JsonPropertyName("repayments")]
    public List<Repayment> Repayments { get; set; } = new();
}

public sealed class Problem
{
    [JsonPropertyName("loan_id")]
    public string LoanId { get; set; } = default!;

    [JsonPropertyName("details")]
    public string Details { get; set; } = default!;

    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [JsonPropertyName("severity")]
    public string Severity { get; set; } = default!; // enum: "error" | "warning"
}

public sealed class RepaymentResponse
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;

    [JsonPropertyName("upload_id")]
    public int UploadId { get; set; }

    [JsonPropertyName("problems")]
    public List<Problem> Problems { get; set; } = new();
}

// ---------- Loan Draft ----------
public sealed class Entreps
{
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = default!;

    [JsonPropertyName("loan_id")]
    public string LoanId { get; set; } = default!;

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = default!;

    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = default!;

    [JsonPropertyName("gender")]
    public string Gender { get; set; } = default!;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}

public sealed class Schedule
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = default!; // example: "2020-12-30"

    [JsonPropertyName("principal")]
    public decimal Principal { get; set; }

    [JsonPropertyName("interest")]
    public decimal Interest { get; set; }
}

public sealed class LoanDraftRequest
{
    [JsonPropertyName("description_language_id")]
    public int DescriptionLanguageId { get; set; }

    [JsonPropertyName("activity_id")]
    public int ActivityId { get; set; }

    [JsonPropertyName("theme_type_id")]
    public int ThemeTypeId { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; } = default!;

    [JsonPropertyName("internal_client_id")]
    public string? InternalClientId { get; set; } // group loans

    [JsonPropertyName("internal_loan_id")]
    public string? InternalLoanId { get; set; } // group loans

    [JsonPropertyName("group_name")]
    public string? GroupName { get; set; } // group loans

    [JsonPropertyName("client_waiver_signed")]
    public bool ClientWaiverSigned { get; set; }

    [JsonPropertyName("loanuse")]
    public string LoanUse { get; set; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("entreps")]
    public List<Entreps> Entreps { get; set; } = new();

    [JsonPropertyName("not_pictured")]
    public List<bool>? NotPictured { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = default!;

    [JsonPropertyName("disburse_time")]
    public string DisburseTime { get; set; } = default!;

    [JsonPropertyName("image_encoded")]
    public string ImageEncodedBase64 { get; set; } = default!; // base64 image

    [JsonPropertyName("schedule")]
    public List<Schedule> Schedule { get; set; } = new();
}

public sealed class LoanDraftResponse
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;

    [JsonPropertyName("loanDraftUuid")]
    public string? LoanDraftUuid { get; set; }
}

// ---------- Journals ----------
public sealed class Journal
{
    [JsonPropertyName("internal_loan_id")]
    public string InternalLoanId { get; set; } = default!;

    [JsonPropertyName("internal_client_id")]
    public string? InternalClientId { get; set; }

    [JsonPropertyName("subject")]
    public string Subject { get; set; } = default!;

    [JsonPropertyName("body")]
    public string Body { get; set; } = default!;

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; } = default!;
}

public sealed class JournalRequest
{
    [JsonPropertyName("journals")]
    public List<Journal> Journals { get; set; } = new();
}

public sealed class JournalResponse
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;

    [JsonPropertyName("archive_id")]
    public string ArchiveId { get; set; } = default!;

    [JsonPropertyName("confirm_url")]
    public string ConfirmUrl { get; set; } = default!;
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