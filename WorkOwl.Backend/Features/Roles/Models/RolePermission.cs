using WC.Backend.Features.Users.Models;

namespace WC.Backend.Features.Roles.Models;

/// <summary>
/// Kobling mellom en rolle og en permission
/// Bestemmer hvilke handlinger en rolle har tilattelse til i systemet
/// </summary>
public class RolePermission
{
    // ========================= Primary Keys =========================
    /// <summary>
    /// Compound primary key med ID-ene til en Role og en Permission
    /// </summary>
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
    
    // ========================= Historikk =========================
    /// <summary>
    /// Brukeren som tildelte permission til en rolle for historikk
    /// </summary>
    public string AssignedById { get; set; } = string.Empty;
    
    /// <summary>
    /// DateTime når permission ble tildelt
    /// </summary>
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    
    // ========================= Navigasjonsegenskaper =========================
    public Role Role { get; set; } = null!;
    public Permission Permission { get; set; } = null!;
    public AppUser AssignedBy { get; set; } = null!;
}   