using Data;
using Data.Interfaces;
using Entity;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; }
        public IStaffRepository Staff { get; }
        public IBookRepository Books { get; }
        public ILoanRepository Loans { get; }
        public IFineRepository Fines { get; }
        public IReservationRepository Reservations { get; }
        public IGenreRepository Genres { get; }
        public IPublisherRepository Publishers { get; }

        public UnitOfWork(

            AppDbContext context,
            IUserRepository userRepository,
            IStaffRepository staffRepository,
            IBookRepository bookRepository,
            ILoanRepository loanRepository,
            IFineRepository fineRepository,
            IReservationRepository reservationRepository,
            IGenreRepository genreRepository,
            IPublisherRepository publisherRepository
        )
        {
            _context = context;

            Users = userRepository;
            Staff = staffRepository;
            Books = bookRepository;
            Loans = loanRepository;
            Fines = fineRepository;
            Reservations = reservationRepository;
            Genres = genreRepository;
            Publishers = publisherRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
