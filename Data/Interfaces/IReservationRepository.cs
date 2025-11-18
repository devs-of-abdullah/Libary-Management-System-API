using Entity;


namespace Data.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation?> GetByIdAsync(int id);
        Task<Reservation> AddAsync(Reservation reservation);
        Task<Reservation?> UpdateAsync(Reservation reservation);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Reservation>> GetPendingReservationsAsync();
    }
}
