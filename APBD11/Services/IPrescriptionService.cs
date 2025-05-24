using APBD11.DTOs;

namespace APBD11.Services;

public interface IPrescriptionService
{
    Task<int> AddPrescriptionAsync(PrescriptionRequestDTO prescriptionRequest);
}