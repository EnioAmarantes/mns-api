using Application.Requests;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SupplierController : ControllerBase
{
    private readonly ILogger<SupplierController> _logger;
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService, ILogger<SupplierController> logger)
    {
        _supplierService = supplierService;
        _logger = logger;
    }

    private Guid GetCompanyIdFromClaims()
    {
        var companyIdClaim = User.FindFirst("companyId")?.Value;

        Console.WriteLine($"Extracted companyId claim: {companyIdClaim}");
        return Guid.Parse(companyIdClaim ?? throw new UnauthorizedAccessException("CompanyId claim is missing."));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var suppliers = _supplierService.GetAll(companyId);
            return Ok(suppliers);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching suppliers.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var supplier = _supplierService.GetById(companyId, id);
            return Ok(supplier);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching suppliers.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] SupplierRequest request)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            Console.WriteLine($"Creating supplier: {request}");
            var supplier = _supplierService.Create(companyId, request);
            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the supplier.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] SupplierRequest request)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            Console.WriteLine($"Updating supplier ID: {id} with data: {request}");
            var updatedSupplier = _supplierService.Update(companyId, id, request);
            return Ok(updatedSupplier);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the supplier.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            _supplierService.Delete(companyId, id);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the supplier.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }
}