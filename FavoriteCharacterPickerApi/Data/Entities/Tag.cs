namespace FavoriteCharacterPickerApi.Data.Entities;

public class Tag
{
    public int Id { get; set; }
    public string TagName { get; set; }
    
    public List<Quiz> QuizzesContaining { get; set; }
}