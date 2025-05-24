using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace APBD11.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}