using Data.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly AppDbContext _context;

        public StaffRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            return await _context.Staff.Where(s => s.IsActive).ToListAsync();
        }

        public async Task<Staff?> GetByIdAsync(int id)
        {
            return await _context.Staff.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Staff?> GetByUsernameAsync(string username)
        {
            return await _context.Staff.FirstOrDefaultAsync(s => s.Username == username);
        }

        public async Task<Staff> AddAsync(Staff staff)
        {
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();  
            return staff; 
        }

        public async Task<Staff?> UpdateAsync(Staff staff)
        {
            var existing = await _context.Staff.FindAsync(staff.Id);
            if (existing == null) return null;

            existing.FirstName = staff.FirstName;
            existing.LastName = staff.LastName;
            existing.Username = staff.Username;
            existing.HashedPassword = staff.HashedPassword;
            existing.Role = staff.Role;
            existing.HireDate = staff.HireDate;
            existing.IsActive = staff.IsActive;

            _context.Staff.Update(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) return false;

            _context.Staff.Remove(staff);
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) return false;

            staff.IsActive = false;
            _context.Staff.Update(staff);

            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Staff.AnyAsync(s => s.Id == id);
        }
    }
}
