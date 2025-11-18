using Data.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Genre).Include(b => b.Publisher).Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author).Include(b => b.Loans).Include(b => b.Reservations) .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Genre).Include(b => b.Publisher).Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author).Include(b => b.Loans).Include(b => b.Reservations).FirstOrDefaultAsync(b => b.Id == id);
        }

        public Task<Book?> AddAsync(Book book)
        {
            _context.Books.Add(book);

            return Task.FromResult<Book?>(book);

        }

        public async Task<Book?> UpdateAsync(Book book)
        {
            var existing = await _context.Books.FindAsync(book.Id);
            if (existing == null) return null;

            existing.Title = book.Title;
            existing.ISBN = book.ISBN;
            existing.PublicationYear = book.PublicationYear;
            existing.CopiesTotal = book.CopiesTotal;
            existing.CopiesAvailable = book.CopiesAvailable;
            existing.GenreId = book.GenreId;
            existing.PublisherId = book.PublisherId;

            _context.Books.Update(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }
    }
}
