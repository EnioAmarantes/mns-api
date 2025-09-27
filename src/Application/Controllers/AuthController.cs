using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Requests;
using Infra.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new { message = "Email e senha são obrigatórios" });
        }

        var user = _context.Users.Include(u => u.Company).FirstOrDefault(u => u.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Credenciais inválidas" });
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("companyId", user.CompanyId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Ok(new
        {
            token = tokenHandler.WriteToken(token),
            companyId = user.CompanyId,
            companyName = user.Company.Name,
            name = user.Name,
            userId = user.Id,
            email = user.Email,
            mustChangePassword = user.MustChangePassword
        });
    }

    [HttpPost("change-password")]
    public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.NewPassword))
        {
            return BadRequest(new { message = "Email e nova senha são obrigatórios" });
        }

        var user = _context.Users.AsNoTracking().FirstOrDefault(u => u.Email == request.Email);
        if (user == null)
        {
            return NotFound(new { message = "Usuário não encontrado" });
        }

        if (!user.MustChangePassword)
        {
            return BadRequest(new { message = "Usuário não precisa renovar a senha" });
        }

        var newUserPass = new User{
            Id = user.Id,
            CompanyId = user.CompanyId,
            Company = user.Company,
            Name = user.Name,
            Email = user.Email,
            PasswordHash =  BCrypt.Net.BCrypt.HashPassword(request.NewPassword),
            MustChangePassword = false
        };

        _context.Users.Update(newUserPass);
        _context.SaveChanges();

        return Ok(new { message = "Senha alterada com sucesso" });
    }
}