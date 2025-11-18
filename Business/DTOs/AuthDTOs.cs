namespace Business.DTOs
{
    public class StaffRegisterDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = "admin";
    }
    public class StaffLoginDto
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }


}
