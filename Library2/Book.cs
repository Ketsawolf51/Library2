using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Library2;

public class Book
{
    [Key]
    public Guid Id { get; set; }
    public string  Name{ get; set; }
    
    
    public string Genre { get; set; }
    public int Year_Publication { get; set; } = DateTime.Now.Year;
    public string ISBN { get; set; }
    
    [ForeignKey("AuthorID")]
    public Guid AuthorID { get; set; } = Guid.Empty;
    [JsonIgnore]
    public Author Author { get; set; }
    
    public int Count { get; set; }
    
    [JsonIgnore]
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}