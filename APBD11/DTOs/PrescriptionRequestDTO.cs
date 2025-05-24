namespace APBD11.DTOs;

public class PrescriptionRequestDTO
{
    public PatientRequestDTO Patient { get; set; }
    public List<MedicamentDTO> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}