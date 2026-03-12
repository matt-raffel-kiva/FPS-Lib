using System.Text.Json.Serialization;
namespace Kiva.FPS.Lib.Contracts;


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
