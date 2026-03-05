using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using WC.Backend.Features.Competencies.Models;
using WC.Backend.Features.Roles.Models;
using WC.Backend.Features.Users.Enums;

namespace WC.Backend.Features.Users.Models;

/// <summary>
/// En bruker av systemet
/// </summary>
public class AppUser : IdentityUser
{
    // ========================= Identity Egenskaper =========================
  
    // - Id (string)
    // - UserName (string)
    // - Email (string)
    // - EmailConfirmed (bool)
    // - PasswordHash (string)
    // - PhoneNumber (string)
    // - PhoneNumberConfirmed (bool)
    // - TwoFactorEnabled (bool)
    // - LockoutEnd (DateTimeOffset?)
    // - LockoutEnabled (bool)
    // - AccessFailedCount (int)
    
    // ========================= Foreign keys =========================
    /// <summary>
    /// Sjefen til brukeren. Er nullable da de som sitter på toppen ikke har noen over seg igjen
    /// </summary>
    [StringLength(100, MinimumLength = 1)]
    public string? ManagerId { get; set; }
    
    /// <summary>
    /// Avdelingen til brukeren. Er nullable da brukeren kan være ansatt, men ikke ha en avdeling
    /// </summary>
    public int? DepartmentId { get; set; }
    
    // ========================= User =========================
    
    /// <summary>
    /// Fornavn på brukeren. Må være mellom 1-100 tegn
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// Etternavn på brukeren. Må være mellom 1-100 tegn
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// Jobbtittelen til brukeren. Må være mellom 1-100 tegn. Eks: Lagerarbeider, Renholder
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// Ansettelsestype på brukeren. Fast, Vikariat eller Innleid
    /// </summary>
    public EmploymentType EmploymentType { get; set; }
    
    // ========================= Opprettelse egenskaper =========================
    /// <summary>
    /// Brukeren som opprettet en annen bruker for historikk
    /// </summary>
    public string? CreatedById { get; set; }
    
    /// <summary>
    /// DateTime-objekt når brukeren ble opprettet
    /// </summary> 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    
    // ========================= Soft delete-egenskaper =========================
    
    /// <summary>
    /// Spesifiserer om brukeren er aktiv eller slettet (soft deleted). Standard 'true' ved opprettelse
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// DateTime-objekt når brukeren ble satt som slettet. 
    /// </summary>
    public DateTime? DeletedAt { get; set; }
    
    /// <summary>
    /// Brukeren som soft-deleted brukeren for historikk
    /// </summary>
    public string? DeletedById { get; set; }
    
    

    // ========================= Navigasjonsegenskaper =========================
    
    /// <summary>
    /// En bruker kan ha flere roller med egne tilattelser
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = [];
    
    /// <summary>
    /// En bruker kan ha flere kompetansebevis
    /// </summary>
    public ICollection<Competency> Competencies { get; set; } = [];
    
    public AppUser? Manager { get; set; }
    public AppUser? CreatedBy { get; set; }
    public AppUser? DeletedBy { get; set; }
    public Department? Department { get; set; }
}