using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FinanceController : ControllerBase
{
    private readonly ILogger<FinanceController> _logger;
    private readonly IFinanceService _financeService;

    public FinanceController(ILogger<FinanceController> logger, IFinanceService financeService)
    {
        _logger = logger;
        _financeService = financeService;
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
            var finances = _financeService.GetAll(companyId);
            return Ok(finances);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching finances.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            if (companyId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid company ID." });
            }
            var finance = _financeService.GetById(companyId, id);
            return Ok(finance);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching finances.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] FinanceRequest finance)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            if (companyId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid company ID." });
            }
            var createdFinance = _financeService.Create(companyId, finance);
            return CreatedAtAction(nameof(GetById), new { id = createdFinance.Id }, createdFinance);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating finance.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] FinanceRequest finance)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            if (companyId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid company ID." });
            }
            var updatedFinance = _financeService.Update(companyId, id, finance);
            return Ok(updatedFinance);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating finance.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            if (companyId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid company ID." });
            }
            _financeService.Delete(id, companyId);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting finance.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }
}