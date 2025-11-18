
namespace Entity
{
    public class Reservation
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public int UserId { get; set; }

        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReadyForPickupDate { get; set; } 
        public string? Status { get; set; } 

        public Book? Book { get; set; }
        public User? User { get; set; }
    }
}