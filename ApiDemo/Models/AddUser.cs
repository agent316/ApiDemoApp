namespace ApiDemo.Models
{
    public class AddUser
    {
        public string Name { get; set; }
        public string? Gender { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Cookie { get; set; }
      
    }
}
