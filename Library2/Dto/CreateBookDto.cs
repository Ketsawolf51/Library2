namespace Library2.Dto;

public class CreateBookDto
{
    public string Isbn { get; set; } 
    public string Name { get; set; }
    public string AuthorFirstName { get; set; } 
    public string AuthorLastName { get; set; } 
    public int Year { get; set; }
    public string Genre { get; set; } 
    public int Count { get; set; }
}