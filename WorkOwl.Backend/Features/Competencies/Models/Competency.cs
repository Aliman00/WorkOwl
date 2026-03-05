using System.ComponentModel.DataAnnotations;
using WC.Backend.Features.Competencies.Enums;
using WC.Backend.Features.Users.Models;

namespace WC.Backend.Features.Competencies.Models;

/// <summary>
/// Et kompetansebevis, av en CompetanceType, for en bruker
/// </summary>
public class Competency
{
    // ========================= Primary Key =========================
    /// <summary>
    /// Primærnøkkel
    /// </summary>
    public int Id { get; set; }
    
    // ========================= Foreign Key =========================
    /// <summary>
    /// Brukeren som eier kompetansebeviset
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    
    /// <summary>
    /// KompetanseType 
    /// </summary>
    public int TypeId { get; set; }
    
    // ========================= Competencyk =========================
    
    /// <summary>
    /// Status til et kompetansebevis 
    /// </summary>
    public CompetencyStatus Status { get; set; }
    
    /// <summary>
    /// URL-en til et tilhørende dokument/kursbevis
    /// </summary>
    [StringLength(2048)]
    public string? DocumentUrl { get; set; }
    
    /// <summary>
    /// Notater tilhørende kursbeviset
    /// </summary>
    [StringLength(500)]
    public string? Notes { get; set; }
    
    // ========================= Opprettelse og historikk =========================
    
    /// <summary>
    /// Når kompetansebeviset går ut. Nullable da ikke alle bevis går ut
    /// </summary>
    public DateTime? ExpiryDate { get; set; }
    
    
    /// <summary>
    /// Brukeren som opprettet kompetansebeviset for historikk
    /// </summary>
    public string CreatedById { get; set; } = string.Empty;
    
    /// <summary>
    /// DateTime når kompetansebeviset ble opprettet
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Når kompetansebeviset ble utgitt
    /// </summary>
    public DateTime IssuedDate { get; set; }
    
    
    
    // ========================= Navigasjonsegenskaper =========================
    public AppUser User { get; set; } = null!;
    public AppUser CreatedBy { get; set; } = null!;
    public CompetencyType Type { get; set; } = null!;
}