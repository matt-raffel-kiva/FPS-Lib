using System.Text.Json.Serialization;
namespace Kiva.FPS.Lib.Contracts;


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
