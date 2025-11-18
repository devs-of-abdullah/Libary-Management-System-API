using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }

        public DateTime LoanDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public ICollection<Fine> Fines { get; set; } = new List<Fine>();
        public Book? Book { get; set; }
        
        public User? User { get; set; }

        public bool IsOverdue => ReturnDate == null && DateTime.UtcNow > DueDate;
    }
}