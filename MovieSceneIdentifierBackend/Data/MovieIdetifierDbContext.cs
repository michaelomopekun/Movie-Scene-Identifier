using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


public class MovieIdentifierDbContext : DbContext
{

    public MovieIdentifierDbContext(DbContextOptions<MovieIdentifierDbContext> options) : base(options){ }

    public DbSet<Movie>? Movies { get; set; }
    public DbSet<UploadedClip>? UploadedClips { get; set; }
    public DbSet<MovieIdentified>? MoviesIdentified { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var stringListConverter = new ValueConverter<List<string>, string>(
            v => string.Join(',', v ?? new List<string>()),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

        modelBuilder.Entity<Movie>(entity =>
        { });
        modelBuilder.Entity<UploadedClip>(entity =>
        { });
        modelBuilder.Entity<MovieIdentified>(entity =>
        { });
    }

}