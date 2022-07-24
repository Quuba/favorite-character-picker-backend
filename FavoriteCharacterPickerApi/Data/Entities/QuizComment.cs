namespace FavoriteCharacterPickerApi.Data.Entities;

public class QuizComment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int ParentId { get; set; }
    public QuizComment Parrent { get; set; }
    public int AuthorId { get; set; }
    public User Author { get; set; }
}