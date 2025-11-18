using Entity;


namespace Data.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan?> GetByIdAsync(int id);
        Task<Loan> AddAsync(Loan loan);
        Task<Loan?> UpdateAsync(Loan loan);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Loan>> GetOverdueLoansAsync();
    }
}
