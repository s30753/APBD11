using System.ComponentModel.DataAnnotations;

namespace APBD11.Models;

public class Doctor
{
    [Key]
    [Required]
    public int IdDoctor { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; }
}