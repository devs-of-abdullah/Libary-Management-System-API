using Entity;


namespace Data.Interfaces
{
    public interface IBookAuthorRepository
    {
        Task<IEnumerable<BookAuthor>> GetAllAsync();
        Task<BookAuthor?> GetAsync(int bookId, int authorId);
        Task<BookAuthor> AddAsync(BookAuthor bookAuthor);
        Task<bool> RemoveAsync(int bookId, int authorId);
        Task<bool> ExistsAsync(int bookId, int authorId);
    }
}
