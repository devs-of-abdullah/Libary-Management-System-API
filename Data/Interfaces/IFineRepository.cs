using Entity;


namespace Data.Interfaces
{
    public interface IFineRepository
    {
        Task<IEnumerable<Fine>> GetAllAsync();
        Task<Fine?> GetByIdAsync(int id);
        Task<Fine> AddAsync(Fine fine);
        Task<Fine?> UpdateAsync(Fine fine);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Fine>> GetOutstandingFinesAsync();
    }
}
