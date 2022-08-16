using System.ComponentModel.DataAnnotations.Schema;

namespace FavoriteCharacterPickerApi.Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }

    public string Email { get; set; }
    
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }

    [InverseProperty("Author")]
    public List<Quiz> CreatedQuizzse { get; set; }
    
    [InverseProperty("User")]
    public List<QuizResult> QuizResults { get; set; }
    
    public List<User> FollowedUsers { get; set; }
    public List<User> FollowingUsers { get; set; }
    public List<Quiz> FavoriteQuizzes { get; set; }
    
    public List<QuizComment> CreatedComments { get; set; }
}