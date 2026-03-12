namespace Kiva.FPS.Lib.Contracts;

/// <summary>
/// Credentials for the FPS API.
/// ClientId and ClientSecret are provided to you by Kiva.
/// </summary>
public class Credentials
{
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
    public Audience Audience { get; set; } = Audience.Staging;
    public Scopes Scopes { get; set; } = Scopes.CreateJournalUpdate | Scopes.CreateLoanDraft | Scopes.CreateRepayment | Scopes.ReadLoans;
}