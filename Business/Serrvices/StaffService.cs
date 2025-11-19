using Business.Interfaces;
using Data;
using Entity;
using Microsoft.EntityFrameworkCore;

public class StaffService : IStaffService
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwt;

    public StaffService(AppDbContext context, JwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    public async Task<bool> RegisterStaffAsync(string? FirstName, string? LastName, string Username, string Password, string? Role)
    {
        if (await _context.Staff.AnyAsync(s => s.Username == Username))
            return false;

        var staff = new Staff
        {
            FirstName = FirstName,
            LastName = LastName,
            Username = Username,
            HashedPassword = PasswordHelper.HashPassword(Password),
            Role = Role ?? string.Empty,
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