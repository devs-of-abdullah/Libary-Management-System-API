using Entity;
using Microsoft.EntityFrameworkCore;
using Data.Interfaces;

namespace Data.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;

        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans.Include(l => l.Book).Include(l => l.User).Include(l => l.Fines).ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(int id)
        {
            return await _context.Loans.Include(l => l.Book).Include(l => l.User).Include(l => l.Fines).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Loan> AddAsync(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return loan; 
        }

        public async Task<Loan?> UpdateAsync(Loan loan)
        {
            var existing = await _context.Loans.FindAsync(loan.Id);
            if (existing == null) return null;

            existing.BookId = loan.BookId;
            existing.UserId = loan.UserId;
            existing.LoanDate = loan.LoanDate;
            existing.DueDate = loan.DueDate;
            existing.ReturnDate = loan.ReturnDate;

            _context.Loans.Update(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null) return false;

            _context.Loans.Remove(loan);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Loans.AnyAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
        {
            return await _context.Loans.Include(l => l.Book).Include(l => l.User).Include(l => l.Fines)
                .Where(l => l.ReturnDate == null && DateTime.UtcNow > l.DueDate).ToListAsync();
        }

    }
}
