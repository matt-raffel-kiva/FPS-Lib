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