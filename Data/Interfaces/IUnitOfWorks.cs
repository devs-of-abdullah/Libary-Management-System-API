using Data.Interfaces;

namespace DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IStaffRepository Staff { get; }
        IBookRepository Books { get; }
        ILoanRepository Loans { get; }
        IFineRepository Fines { get; }
        IReservationRepository Reservations { get; }
        IGenreRepository Genres { get; }
        IPublisherRepository Publishers { get; }

        Task<int> CompleteAsync();
    }
}
