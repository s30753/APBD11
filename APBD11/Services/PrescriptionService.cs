using APBD11.Data;
using APBD11.DTOs;
using APBD11.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace APBD11.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly DatabaseContext _context;

    public PrescriptionService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> AddPrescriptionAsync(PrescriptionRequestDTO prescriptionRequest)
    {
        if (prescriptionRequest.Medicaments.Count > 0) throw new Exception("Too many medicaments");
        if (prescriptionRequest.DueDate < prescriptionRequest.Date) throw new Exception("Incorrect dates");
        
        var patient = await _context.Patients.FirstOrDefaultAsync(p =>
            p.FirstName == prescriptionRequest.Patient.FirstName 
            && p.LastName == prescriptionRequest.Patient.LastName 
            && p.Birthdate == prescriptionRequest.Patient.Birthdate);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescriptionRequest.Patient.FirstName,
                LastName = prescriptionRequest.Patient.LastName,
                Birthdate = prescriptionRequest.Patient.Birthdate,
            };
             _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }
        
        var idsOfMedicaments = prescriptionRequest.Medicaments.Select(m => m.IdMedicament).ToList();
        foreach (var id in idsOfMedicaments)
        {
            if (!_context.Medicaments.Where(m => m.IdMedicament == id).Any())
            {
                throw new Exception("Medicament not found");
            }
        }

        var prescription = new Prescription
        {
            Date = prescriptionRequest.Date,
            DueDate = prescriptionRequest.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = 1,
            PrescriptionMedicaments = prescriptionRequest.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            }).ToList()
        };
        
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
        return 0;
    }
}