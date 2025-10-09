using Application.Requests;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryService _categoryService;

    public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
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
            var categories = _categoryService.GetAll(companyId);
            return Ok(categories);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching categories.");
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
            var category = _categoryService.GetById(companyId, id);
            return Ok(category);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the category.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] CategoryRequest category)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            if (companyId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid company ID." });
            }
            var createdCategory = _categoryService.Create(companyId, category);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the category.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] CategoryRequest category)
    {
        try
        {
            var companyId = GetCompanyIdFromClaims();
            if (companyId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid company ID." });
            }
            var updatedCategory = _categoryService.Update(companyId, id, category);
            return Ok(updatedCategory);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the category.");
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
            _categoryService.Delete(id, companyId);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the category.");
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }
}