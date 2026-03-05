using System.ComponentModel.DataAnnotations;

namespace WC.Backend.Features.Roles.Models;

/// <summary>
/// En permission gir tilattelse til å utføre en handling i systemets funksjonaliteter.
/// Permissions kan være: competencies:read, documents:delete, onboarding:write
/// </summary>
public class Permission
{
    // ========================= Primary Key =========================
    /// <summary>
    /// Primærnøkkel
    /// </summary>
    public int Id { get; set; }
    
    // ========================= Role =========================
    /// <summary>
    /// Navnet på en permission. Må være mellom 1-50 tegn. Eks: competencies:read, documents:delete
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// En valgfritt beskrivelse av permission. Eks: "Gir tilattelse til å opprette et sertifikat"
    /// </summary>
    [StringLength(250)]
    public string? Description { get; set; }
}