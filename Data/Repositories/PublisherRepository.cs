using Data.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _context;

        public PublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await _context.Publishers.Include(p => p.Books).ToListAsync();
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _context.Publishers.Include(p => p.Books).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Publisher> AddAsync(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return publisher; 
        }

        public async Task<Publisher?> UpdateAsync(Publisher publisher)
        {
            var existing = await _context.Publishers.FindAsync(publisher.Id);
            if (existing == null) return null;

            existing.Name = publisher.Name;
            existing.City = publisher.City;

            _context.Publishers.Update(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null) return false;

            _context.Publishers.Remove(publisher);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Publishers.AnyAsync(p => p.Id == id);
        }
    }
}
