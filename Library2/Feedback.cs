using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Library2;

public class Feedback
{
    [Key]
    public Guid id { get; set; }
    [ForeignKey("BookId")]
    public Guid BookId { get; set; }
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }
    
    [Range(1, 5, ErrorMessage = "рейтинг должен быть в диапазоне от 1 до 5.")]
    public int Rating { get; set; }
    
    [StringLength(1000, ErrorMessage = "Комментарий не может превышать 1000 символов.")]
    public string Comment { get; set; }
    
    [JsonIgnore]
    public Book Book { get; set; }
    
    [JsonIgnore]
    public User User { get; set; }
}