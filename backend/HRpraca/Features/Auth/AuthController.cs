using System.Security.Claims;
using HRpraca.DAL;
using Microsoft.AspNetCore.Mvc;
using HRpraca.Features.Auth;
using HRpraca.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace HRpraca.Features.Auth;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(
        AppDbContext db,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _db = db;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == request.Email);
        
        if (user == null)
            return  Unauthorized("Invalid credentials");
        
        if (!user.IsActive)
            return Unauthorized("User is Inactive");
        
        var passwordValid = _passwordHasher.Verify(
            request.Password,
            user.PasswordHash);
        
        if (!passwordValid)
            return Unauthorized("Invalid credentials");

        var token = _jwtTokenService.GenerateJwtToken(user);

        return Ok(new LoginResponse(token));

    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub),
            Email = User.FindFirstValue(JwtRegisteredClaimNames.Email),
            Role = User.FindFirstValue(ClaimTypes.Role)
        });
    }


}