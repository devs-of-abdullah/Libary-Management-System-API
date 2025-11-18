using Entity;
using Microsoft.EntityFrameworkCore;
using Data.Interfaces;
namespace Data.Repositories
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        private readonly AppDbContext _context;

        public BookAuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookAuthor>> GetAllAsync()
        {
            return await _context.BookAuthors.Include(ba => ba.Book).Include(ba => ba.Author).ToListAsync();
        }

        public async Task<BookAuthor?> GetAsync(int bookId, int authorId)
        {
            return await _context.BookAuthors.Include(ba => ba.Book).Include(ba => ba.Author).FirstOrDefaultAsync(ba => ba.BookId == bookId && ba.AuthorId == authorId);
        }

        public async Task<BookAuthor> AddAsync(BookAuthor bookAuthor)
        {
            _context.BookAuthors.Add(bookAuthor);
            await _context.SaveChangesAsync();
            return bookAuthor; 
        }

        public async Task<bool> RemoveAsync(int bookId, int authorId)
        {
            var entity = await _context.BookAuthors.FindAsync(bookId, authorId);
            if (entity == null) return false;

            _context.BookAuthors.Remove(entity);
            return true;
        }

        public async Task<bool> ExistsAsync(int bookId, int authorId)
        {
            return await _context.BookAuthors.AnyAsync(ba => ba.BookId == bookId && ba.AuthorId == authorId);
        }
    }
}
