using APBD11.Data;
using APBD11.DTOs;
using APBD11.Models;

namespace APBD11.Services;

public interface IPatientService
{
    Task<List<PatientDTO>> GetPatientDataAsync(int id);

}