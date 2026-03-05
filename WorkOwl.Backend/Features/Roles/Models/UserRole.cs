using WC.Backend.Features.Users.Models;

namespace WC.Backend.Features.Roles.Models;

/// <summary>
/// Kobling mellom en user og en rolle
/// Bestemmer hvilke roller en bruker har, som igjen bestemmer hvilke tilattelser brukeren har
/// </summary>
public class UserRole
{
    // ========================= Primary Keys =========================
    /// <summary>
    /// Compound primary key med ID-ene til en User og en Role
    /// </summary>
    public string UserId { get; set; } = string.Empty;
    public int RoleId { get; set; }
    
    // ========================= Historikk =========================
    /// <summary>
    /// Brukeren som tildelte rollen for historikk
    /// </summary>
    public string AssignedById { get; set; } = string.Empty;
    
    /// <summary>
    /// DateTime når rollen ble tildelt
    /// </summary>
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    
    // ========================= Navigasjonsegenskaper =========================
    public AppUser User { get; set; } = null!;
    public Role Role { get; set; } = null!;
    public AppUser AssignedBy { get; set; } = null!;
    
}