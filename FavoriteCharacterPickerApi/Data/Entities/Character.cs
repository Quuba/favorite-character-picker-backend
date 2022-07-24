namespace FavoriteCharacterPickerApi.Data.Entities;

public class Character
{
    public int Id { get; set; }
    public string CharacterName { get; set; }
    public int TitleId { get; set; }
    public Title Title { get; set; }
    public bool IsVerified { get; set; }
}