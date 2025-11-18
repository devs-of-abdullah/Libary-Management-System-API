using Entity;


namespace Data.Interfaces
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetAllAsync();
        Task<Staff?> GetByIdAsync(int id);
        Task<Staff?> GetByUsernameAsync(string username);
        Task<Staff> AddAsync(Staff staff);
        Task<Staff?> UpdateAsync(Staff staff);
        Task<bool> DeleteAsync(int id); 
        Task<bool> SoftDeleteAsync(int id); 
        Task<bool> ExistsAsync(int id);
    }
}
