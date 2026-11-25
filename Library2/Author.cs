using System.ComponentModel.DataAnnotations;

namespace Library2;

public class Author
{
    [Key]
    public Guid AuthorID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Biography { get; set; }
    public int YearOfBirth { get; set; }
}