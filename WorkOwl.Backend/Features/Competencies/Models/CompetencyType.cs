using System.ComponentModel.DataAnnotations;
using WC.Backend.Features.Users.Models;

namespace WC.Backend.Features.Competencies.Models;

/// <summary>
/// En type kompetansebevis. Eks: "Truckførerbevis", "Fallsikringskurs", "Varme arbeider"
/// </summary>
public class CompetencyType
{
    // ========================= Primary Key =========================
    /// <summary>
    /// Primærnøkkel
    /// </summary>
    public int Id { get; set; }
    
    // ========================= Role =========================
    /// <summary>
    /// Navnet på kompetansebevis. Må være mellom 1-50 tegn.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// En valgfritt beskrivelse av kompetansebeviset. Eks: "Kreves for å operere truck i sone A og B"
    /// </summary>
    [StringLength(250)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Antall måneder kompetansebevis er gyldig
    /// </summary>
    public int? ValidityMonths { get; set; }
    
    /// <summary>
    /// Forteller om kompetansebevis er internt eller et formelt bevis
    /// </summary>
    public bool IsInternal { get; set; }
    
    
    // ========================= Opprettelse og historikk =========================
    /// <summary>
    /// Brukeren som opprettet kompetansebevistypen for historikk
    /// </summary>
    public string CreatedById { get; set; } = string.Empty;
    
    /// <summary>
    /// DateTime når kompetansebevistypen ble opprettet
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    
    // ========================= Navigasjonsegenskaper =========================
    public AppUser CreatedBy { get; set; } = null!;
}