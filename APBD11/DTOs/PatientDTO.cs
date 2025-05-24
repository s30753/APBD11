using APBD11.Models;

namespace APBD11.DTOs;

public class PatientDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public List<PrescriptionDTO> Prescriptions { get; set; }
}