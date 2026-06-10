using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRestful.Data;
using WebApiRestful.DTOs;
using WebApiRestful.Models;
using WebApiRestful.Services;

namespace WebApiRestful.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly TokenService _tokenService;

    public AuthController(AppDbContext db, TokenService tokenService)
    {
        _db = db;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (await _db.Users.AnyAsync(u => u.Username == dto.Username))
            return BadRequest("Username đã tồn tại.");

        if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest("Email đã tồn tại.");

        var user = new User
        {
            Username     = dto.Username,
            Email        = dto.Email,
            PasswordHash = HashPassword(dto.Password)
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return Ok(new { message = "Đăng ký thành công!", userId = user.Id });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (user == null || user.PasswordHash != HashPassword(dto.Password))
            return Unauthorized("Sai username hoặc password.");

        var (token, expiresAt) = _tokenService.GenerateToken(user);
        return Ok(new AuthResponseDto
        {
            Token     = token,
            Username  = user.Username,
            ExpiresAt = expiresAt
        });
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }
}