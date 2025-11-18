using Data.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.Include(r => r.Book).Include(r => r.User).ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _context.Reservations.Include(r => r.Book).Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Reservation> AddAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation; 
        }

        public async Task<Reservation?> UpdateAsync(Reservation reservation)
        {
            var existing = await _context.Reservations.FindAsync(reservation.Id);
            if (existing == null) return null;

            existing.BookId = reservation.BookId;
            existing.UserId = reservation.UserId;
            existing.ReservationDate = reservation.ReservationDate;
            existing.ReadyForPickupDate = reservation.ReadyForPickupDate;
            existing.Status = reservation.Status;

            _context.Reservations.Update(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;

            _context.Reservations.Remove(reservation);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Reservations.AnyAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reservation>> GetPendingReservationsAsync()
        {
            return await _context.Reservations.Include(r => r.Book).Include(r => r.User).Where(r => r.Status == "Pending").ToListAsync();
        }
    }
}
