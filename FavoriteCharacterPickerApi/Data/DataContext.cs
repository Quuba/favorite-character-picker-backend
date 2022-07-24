using FavoriteCharacterPickerApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FavoriteCharacterPickerApi.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Character> Characters { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<QuizComment> QuizComments { get; set; }
    public DbSet<QuizResult> QuizResults { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<User> Users { get; set; }
    
}