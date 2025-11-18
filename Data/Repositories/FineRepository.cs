using Entity;
using Microsoft.EntityFrameworkCore;
using Data.Interfaces;

namespace Data.Repositories
{
    public class FineRepository : IFineRepository
    {
        private readonly AppDbContext _context;

        public FineRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fine>> GetAllAsync()
        {
            return await _context.Fines.Include(f => f.Loan).ThenInclude(l => l.User).ToListAsync();
        }

        public async Task<Fine?> GetByIdAsync(int id)
        {
     
            return await _context.Fines.Include(f => f.Loan).ThenInclude(l => l.User).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fine> AddAsync(Fine fine)
        {
            _context.Fines.Add(fine);
            await  _context.SaveChangesAsync();
            return fine; 
        }

        public async Task<Fine?> UpdateAsync(Fine fine)
        {
            var existing = await _context.Fines.FindAsync(fine.Id);
            if (existing == null) return null;

            existing.Amount = fine.Amount;
            existing.DateIssued = fine.DateIssued;
            existing.DatePaid = fine.DatePaid;
            existing.IsPaid = fine.IsPaid;
            existing.LoanId = fine.LoanId;

            _context.Fines.Update(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fine = await _context.Fines.FindAsync(id);
            if (fine == null) return false;

            _context.Fines.Remove(fine);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Fines.AnyAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Fine>> GetOutstandingFinesAsync()
        {
            return await _context.Fines.Include(f => f.Loan).ThenInclude(l => l.User).Where(f => !f.IsPaid && DateTime.UtcNow > f.DateIssued).ToListAsync();
        }
    }
}
