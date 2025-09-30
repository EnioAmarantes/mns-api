using Application.Requests;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
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
            if (companyId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid company ID." });
            }
            var products = _productService.GetAll(companyId);
            return Ok(products);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching products.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var product = _productService.GetById(companyId, id);
            return Ok(product);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the product.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] ProductRequest productRequest)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var createdProduct = _productService.Create(companyId, productRequest);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the product.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] ProductRequest productRequest)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            var updatedProduct = _productService.Update(companyId, id, productRequest);
            return Ok(updatedProduct);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the product.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            _productService.Delete(id, companyId);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the product.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }
}