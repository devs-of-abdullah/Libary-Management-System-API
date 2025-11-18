using Entity;
using Microsoft.EntityFrameworkCore;
using Data.Interfaces;

namespace Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.Include(g => g.Books).ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres.Include(g => g.Books).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre; 
        }

        public async Task<Genre?> UpdateAsync(Genre genre)
        {
            var existing = await _context.Genres.FindAsync(genre.Id);
            if (existing == null) return null;

            existing.Name = genre.Name;
            _context.Genres.Update(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return false;

            _context.Genres.Remove(genre);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }
    }
}
