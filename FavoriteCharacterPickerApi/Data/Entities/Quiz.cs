namespace FavoriteCharacterPickerApi.Data.Entities;

public class Quiz
{
    public int Id { get; set; }
    public string QuizName { get; set; }
    
    public int AuthorId { get; set; }
    public User Author { get; set; }
    
    public DateTime CreationDate { get; set; }
    public bool IsVerified { get; set; }
}