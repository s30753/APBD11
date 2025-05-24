using APBD11.Data;
using APBD11.DTOs;
using APBD11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD11.Services;

public class PatientService : IPatientService
{
    private readonly DatabaseContext _context;

    public PatientService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<PatientDTO>> GetPatientDataAsync(int id)
    {
        var patientInfo = await _context.Patients.Where(p => p.IdPatient == id)
            .Select(p => new PatientDTO
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birthdate = p.Birthdate,
                Prescriptions = p.Prescriptions
                    .OrderBy(ps => ps.DueDate)
                    .Select(ps => new PrescriptionDTO
                    {
                        IdPrescription = ps.IdPrescription,
                        Date = ps.Date,
                        DueDate = ps.DueDate,
                        Medicaments = ps.PrescriptionMedicaments.Select(pm => 
                            new MedicamentDTO
                            {
                                IdMedicament = pm.IdMedicament,
                                Name = pm.Medicament.Name,
                                Dose = pm.Dose,
                                Description = pm.Medicament.Description
                            }).ToList(),
                        Doctor = new DoctorDTO
                        {
                            IdDoctor = ps.IdDoctor,
                            FirstName = ps.Doctor.FirstName,
                        }
                    }).ToList()

            }).ToListAsync();
        return patientInfo;
    }
}