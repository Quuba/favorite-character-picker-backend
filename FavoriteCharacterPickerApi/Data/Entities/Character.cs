using System.ComponentModel.DataAnnotations.Schema;

namespace FavoriteCharacterPickerApi.Data.Entities;

public class Character
{
    public int Id { get; set; }
    public string CharacterName { get; set; }
    public bool IsVerified { get; set; }

    
    [ForeignKey("Title")]
    public int TitleId { get; set; }
    public Title Title { get; set; }
    
    public List<QuizResult> QuizResultsContaining { get; set; }
    public List<Quiz> QuizzesContaining { get; set; }
}