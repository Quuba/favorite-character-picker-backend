using System.ComponentModel.DataAnnotations.Schema;

namespace FavoriteCharacterPickerApi.Data.Entities;

public class Quiz
{
    public int Id { get; set; }
    public string QuizName { get; set; }
    
    [ForeignKey("Author")]
    public int AuthorId { get; set; }
    public User Author { get; set; }
    
    public DateTime CreationDate { get; set; }
    public bool IsVerified { get; set; }
    
    public List<Tag> Tags { get; set; }
    public List<User> UsersWithFavorite { get; set; }
    
    public List<Character> Characters { get; set; }
}