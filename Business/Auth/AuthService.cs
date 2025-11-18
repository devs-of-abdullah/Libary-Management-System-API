using Business.Auth;
using Business.DTOs;
using Data;
using Entity;
using Microsoft.EntityFrameworkCore;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwt;

    public AuthService(AppDbContext context, JwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    public async Task<bool> RegisterStaffAsync(StaffRegisterDto dto)
    {
        if (await _context.Staff.AnyAsync(s => s.Username == dto.Username))
            return false;

        var staff = new Staff
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Username = dto.Username,
            HashedPassword = PasswordHelper.HashPassword(dto.Password),
            Role = dto.Role,
            HireDate = DateTime.UtcNow,
            IsActive = true
        };

        _context.Staff.Add(staff);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var staff = await _context.Staff.FirstOrDefaultAsync(s => s.Username == username);

        if (staff == null)
            return null;

        if (!PasswordHelper.VerifyPassword(password, staff.HashedPassword))
            return null;

        return _jwt.GenerateToken(staff.Id, staff.Role);
    }
}
