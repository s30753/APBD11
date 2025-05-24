using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace APBD11.Models;

public class Patient
{
    [Key]
    [Required]
    public int IdPatient { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public DateOnly Birthdate { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; }
}