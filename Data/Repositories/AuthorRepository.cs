using Entity;
using Microsoft.EntityFrameworkCore;
using Data.Interfaces;
namespace Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.Include(a => a.BookAuthors).ThenInclude(b => b.Book).ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.Include(a => a.BookAuthors).ThenInclude(ba => ba.Book).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author?> AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author?> UpdateAsync(Author author)
        {
            var existing = await _context.Authors.FindAsync(author.Id);
            if (existing == null) return null;

            existing.FirstName = author.FirstName;
            existing.LastName = author.LastName;
            existing.BirthYear = author.BirthYear;

            _context.Authors.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }
    }
}
