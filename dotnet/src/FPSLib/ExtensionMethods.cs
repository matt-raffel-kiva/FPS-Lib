using Kiva.FPS.Lib.Contracts;

namespace Kiva.FPS.Lib;

public static class ExtensionMethods
{
    internal static string ToAudienceString(this Audience audience)
    {
        // TODO we should probably move this to a config file
        return audience switch
        {
            Audience.Production => "https://partner-api.dk1.kiva.org",
            Audience.Staging => "https://partner-api-stage.dk1.kiva.org", 
            _ => throw new ArgumentOutOfRangeException(nameof(audience), audience, null)
        };
    }

    internal static string ToScopeString(this Scopes scopes)
    {
        // TODO we should probably move this to a config file
        string scopeStr = "";
        if (scopes.HasFlag(Scopes.CreateJournalUpdate))
            scopeStr += "create:repayment ";
        if (scopes.HasFlag(Scopes.CreateLoanDraft)) 
            scopeStr += "create:loan_draft ";
        if (scopes.HasFlag(Scopes.CreateRepayment))
            scopeStr += "create:journal_update ";
        if (scopes.HasFlag(Scopes.ReadLoans))
            scopeStr += "read:loans ";
        
        if (string.IsNullOrEmpty(scopeStr))
            throw new KivaDefaultException("invalid scope encountered");

        return scopeStr;

    }
}