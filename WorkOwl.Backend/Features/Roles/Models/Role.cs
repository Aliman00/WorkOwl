using System.ComponentModel.DataAnnotations;
using WC.Backend.Features.Users.Models;

namespace WC.Backend.Features.Roles.Models;

/// <summary>
/// En role definert av bedriften. Eks: "Admin", "Renholdsleder", "Utstyrsansvarlig", "LAge
/// </summary>
public class Role
{
    // ========================= Primary Key =========================
    /// <summary>
    /// Primærnøkkel
    /// </summary>
    public int Id { get; set; }
    
    // ========================= Role =========================
    /// <summary>
    /// Navnet på rollen. Må være mellom 1-50 tegn.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// En valgfritt beskrivelse av rollen. Eks: "Gir tilgang til å lese og registrere utstyr, men ikke slette"
    /// </summary>
    [StringLength(250)]
    public string? Description { get; set; }
    
    // ========================= Role opprettelse =========================
    /// <summary>
    /// Brukeren som opprettet rollen for historikk
    /// </summary>
    public string CreatedById { get; set; } = string.Empty;
    
    /// <summary>
    /// DateTime når rollen ble opprettet
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    
    // ========================= Navigasjonsegenskaper =========================
    /// <summary>
    /// En rolle kan ha flere permissions
    /// </summary>
    public ICollection<RolePermission> Permissions { get; set; } = [];
    public AppUser CreatedBy { get; set; } = null!;
}