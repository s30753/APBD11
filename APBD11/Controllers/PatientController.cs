using APBD11.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace APBD11.Controllers;

[Route("api/patients")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientData(int id)
    {
        var patient = await _patientService.GetPatientDataAsync(id);
        if (patient.IsNullOrEmpty()) return NotFound($"Patient with a given id: {id} not found");
        return Ok(patient);
    }
}