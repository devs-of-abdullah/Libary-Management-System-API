using Entity;


namespace Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetActiveUsersAsync();

        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);

        Task<User> AddAsync(User user);
        Task<User?> UpdateAsync(User user);

        Task<bool> DeleteAsync(int id);       
        Task<bool> SoftDeleteAsync(int id); 

        Task<bool> ExistsAsync(int id);
    }
}
