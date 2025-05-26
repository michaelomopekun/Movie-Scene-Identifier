using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


public class MovieIdentifierDbContext : DbContext
{

    public MovieIdentifierDbContext(DbContextOptions<MovieIdentifierDbContext> options) : base(options) { }

    public DbSet<UploadedClip>? UploadedClips { get; set; }
    public DbSet<MovieIdentified>? MoviesIdentified { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var stringListConverter = new ValueConverter<List<string>, string>(
            v => string.Join(',', v ?? new List<string>()),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());


        modelBuilder.Entity<UploadedClip>(entity =>
        {
            entity.HasOne(d => d.MovieIdentified)
                .WithMany(p => p.UploadedClips)
                .HasForeignKey(d => d.MovieIdentifiedId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        
        modelBuilder.Entity<MovieIdentified>(entity =>
        {
            entity.HasMany(d => d.UploadedClips)
                .WithOne(p => p.MovieIdentified)
                .HasForeignKey(d => d.MovieIdentifiedId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

}
