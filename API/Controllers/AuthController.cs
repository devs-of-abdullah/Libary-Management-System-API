using Business.Auth;
using Business.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth/staff")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(StaffRegisterDto dto)
    {
        var result = await _authService.RegisterStaffAsync(dto);

        if (!result)
            return BadRequest("Username already in use.");

        return Ok("Staff registered.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(StaffLoginDto dto)
    {
        var token = await _authService.LoginAsync(dto.Username, dto.Password);

        if (token == null)
            return Unauthorized("Invalid credentials");

        return Ok(new { Token = token });
    }
}
