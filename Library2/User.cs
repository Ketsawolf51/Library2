using System.ComponentModel.DataAnnotations;

namespace Library2;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    
    public string Role { get; set; } = "User";
    
    public DateTime RegistrationDate { get; set; }= DateTime.UtcNow;
}