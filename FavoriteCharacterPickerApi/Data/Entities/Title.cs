using System.ComponentModel.DataAnnotations.Schema;
using FavoriteCharacterPickerApi.Data.Enums;

namespace FavoriteCharacterPickerApi.Data.Entities;

public class Title
{
    public int Id { get; set; }
    public string TitleName { get; set; }
    public TitleType Type { get; set; }
    public bool IsVerified { get; set; }
    
    [InverseProperty("Title")]
    public List<Character> CharactersContaining { get; set; }
}