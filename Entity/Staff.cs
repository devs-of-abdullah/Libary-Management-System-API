using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Staff
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required] 
        public string? Username { get; set; } 

        [Required]
        public string? HashedPassword { get; set; }

        public string? Role { get; set; } 

        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; } = true;

        public string FullName => $"{FirstName} {LastName}";
    }
}
