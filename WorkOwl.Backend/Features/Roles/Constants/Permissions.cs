namespace WC.Backend.Features.Roles.Constants;

/// <summary>
/// Her setter vi tilattelser til forskjellige resursser/moduler/funksjonaliteter.
/// Sikrer at vi ikke får kompeleringsfeil og at vi har oversikt over hvilken tilattelser som går til hva
/// </summary>
public static class Permissions
{
    public const string CompetenciesRead = "competencies:read";
    public const string CompetenciesWrite = "competencies:write";
    public const string CompetenciesDelete = "competencies:delete";
    
    public const string DocumentsRead = "documents:read";
    public const string DocumentsWrite = "documents:write";
    public const string DocumentsDelete = "documents:delete";
}