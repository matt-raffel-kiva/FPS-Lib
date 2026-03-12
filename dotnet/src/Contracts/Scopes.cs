namespace Kiva.FPS.Lib.Contracts;

/// <summary>
/// Maps to scopes in Autho
/// see https://fps-sdk-portal.web.app/docs/overview/authentication#details-on-scope
/// </summary>
[Flags]
public enum Scopes
{
    /// <summary>
    /// equivalent to "create:journalupdate"
    /// </summary>
    CreateJournalUpdate,
    /// <summary>
    /// equivalent to "create:loandraft"
    /// </summary>
    CreateLoanDraft,
    /// <summary>
    /// equivalent to "create:repayment"
    /// </summary>
    CreateRepayment,
    /// <summary>
    /// equivalent to "read:loans"
    /// </summary>
    ReadLoans,
}