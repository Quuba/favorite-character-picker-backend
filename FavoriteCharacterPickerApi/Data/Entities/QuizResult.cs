namespace FavoriteCharacterPickerApi.Data.Entities;

public class QuizResult
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}