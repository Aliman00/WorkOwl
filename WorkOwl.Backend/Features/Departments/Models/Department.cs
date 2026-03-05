using System.ComponentModel.DataAnnotations;

namespace WC.Backend.Features.Departments.Models;

/// <summary>
/// Avdelinger i bedriften. Bedriften oppretter egne avdelinger selv
/// </summary>
public class Department
{
    // ========================= Primær nøkkel =========================
    /// <summary>
    /// Primærnøkkel
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Navnet på avdelingen. Må være mellom 1-50 tegn
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// En valgfritt beskrivelse av avdelingen. Eks: Renholdsavdeling, IKT-avdeling
    /// </summary>
    [StringLength(250, MinimumLength = 1)]
    public string? Description { get; set; }
}


/// <summary>
/// Avdelinger i bedriften. Bedriften oppretter egne avdelinger selv
/// </summary>
public class Department
{
    // ========================= Primær nøkkel =========================
    public int Id { get; set; }
    
    // ========================= Avdelingsegenskaper =========================
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(250, MinimumLength = 1)]
    public string? Description;
}