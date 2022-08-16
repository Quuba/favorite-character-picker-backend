using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FavoriteCharacterPickerApi.Data.Entities;

public class QuizResult
{
    public int Id { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    public List<Character> Characters { get; set; }
    
}