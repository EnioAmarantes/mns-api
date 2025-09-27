using Application.Requests;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StockController : ControllerBase
{
    private readonly IStockBalanceService _stockBalanceService;
    private readonly IStockMoveService _stockMoveService;
    public StockController(IStockBalanceService stockBalanceService, IStockMoveService stockMoveService)
    {
        _stockBalanceService = stockBalanceService;
        _stockMoveService = stockMoveService;
    }
    private Guid GetCompanyIdFromClaims()
    {
        var companyIdClaim = User.FindFirst("companyId")?.Value;

        Console.WriteLine($"Extracted companyId claim: {companyIdClaim}");
        return Guid.Parse(companyIdClaim ?? throw new UnauthorizedAccessException("CompanyId claim is missing."));
    }

    [HttpGet("balances")]
    public IActionResult GetAllStockBalances()
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var stockBalances = _stockBalanceService.GetAll(companyId);
            return Ok(stockBalances);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpGet("balances/{id:guid}")]
    public IActionResult GetStockBalanceById(Guid id)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var stockBalance = _stockBalanceService.GetById(companyId, id);
            return Ok(stockBalance);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpGet]
    public IActionResult GetAllStockMoves()
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var stockMoves = _stockMoveService.GetAll(companyId);
            return Ok(stockMoves);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetStockMoveById(Guid id)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var stockMove = _stockMoveService.GetById(companyId, id);
            return Ok(stockMove);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult CreateStockMove([FromBody] StockMoveRequest request)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var createdStockMove = _stockMoveService.Create(companyId, request);
            return CreatedAtAction(nameof(GetStockMoveById), new { id = createdStockMove.Id }, createdStockMove);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateStockMove(Guid id, [FromBody] StockMoveRequest request)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var updatedStockMove = _stockMoveService.Update(companyId, id, request);
            return Ok(updatedStockMove);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteStockMove(Guid id)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            _stockMoveService.Delete(companyId, id);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}