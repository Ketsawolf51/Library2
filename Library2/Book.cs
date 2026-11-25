using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Library2;

public class Book
{
    [Key]
    public Guid Id { get; set; }
    public string  Name{ get; set; }
    
    [AllowedValues("Фэнтези","Детектив", "Мистика","Хорор")]
    public string Genre { get; set; }
    public int Year_Publication { get; set; } = DateTime.Now.Year;
    public int ISBN { get; set; }
    
    [ForeignKey("AuthorID")]
    public Guid AuthorID { get; set; } = Guid.Empty;
    [JsonIgnore]
    public Author Author { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Количество экземпляров книги не может быть отрицательным.")]
    public int Count { get; set; }
}