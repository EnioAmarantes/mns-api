using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
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
            var sales = _saleService.GetAll(companyId);
            return Ok(sales);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var sale = _saleService.GetById(id, companyId);
            return Ok(sale);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] Application.Requests.SaleRequest sale)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var newSale = _saleService.Create(companyId, sale);
            return CreatedAtAction(nameof(GetById), new { id = newSale.Id }, newSale);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] Application.Requests.SaleRequest sale)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var updatedSale = _saleService.Update(companyId, id, sale);
            return Ok(updatedSale);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            _saleService.Delete(id, companyId);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while processing your request ." });
        }
    }
}