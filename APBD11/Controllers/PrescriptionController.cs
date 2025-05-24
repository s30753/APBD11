using System.Runtime.InteropServices.JavaScript;
using APBD11.DTOs;
using APBD11.Models;
using APBD11.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD11.Controllers;

[Route("api/prescriptions")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescriptionAsync([FromBody] PrescriptionRequestDTO prescriptionRequest)
    {
        try
        {
            var result = _prescriptionService.AddPrescriptionAsync(prescriptionRequest);
            return Ok("Prescription added successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}