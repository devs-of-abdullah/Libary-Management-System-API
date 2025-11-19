using Entity;

namespace API.DTOs
{
    public class CreateFineDto
    {


        public int LoanId { get; set; }
        public Loan? Loan { get; set; }
        public decimal Amount { get; set; }
    }

    public class FineDto
    {
        public int Id { get; set; }
        public int LoanId { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateIssued { get; set; } = DateTime.UtcNow;

        public DateTime? DatePaid { get; set; }

        public bool IsPaid { get; set; }

        public Loan? Loan { get; set; }

        public bool IsOutstanding => !IsPaid && DateTime.UtcNow > DateIssued;
    }
}
