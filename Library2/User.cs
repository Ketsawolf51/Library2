using System.ComponentModel.DataAnnotations;

namespace Library2;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    [AllowedValues("Admin", "User" , ErrorMessage = "Недопустимое значение роли пользователя.")]
    public string Role { get; set; } = "User";
    
    public DateTime RegistrationDate { get; set; }= DateTime.Now;
}