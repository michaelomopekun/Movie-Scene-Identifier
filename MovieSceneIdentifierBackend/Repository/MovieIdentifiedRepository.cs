
using Microsoft.EntityFrameworkCore;

public class MovieIdentifiedRepository : IMovieIdentifiedRepository
{
    private readonly MovieIdentifierDbContext _context;


    public MovieIdentifiedRepository(MovieIdentifierDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteMovieIdentifiedAsync(string id)
    {
        var movieIdentified = await _context.MoviesIdentified
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movieIdentified == null)
        {
            return false;
        }

        _context.MoviesIdentified.Remove(movieIdentified);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<MovieIdentified>> GetAllMoviesIdentifiedAsync(int pageSize = 100, int pageNumber = 1)
    {
        var allMoviesIdentified = await _context.MoviesIdentified
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (allMoviesIdentified == null || allMoviesIdentified.Count == 0)
        {
            return Enumerable.Empty<MovieIdentified>();
        }

        return allMoviesIdentified;
    }

    public async Task<MovieIdentified?> GetMovieIdentifiedByFileNameAsync(string filename, int top_k = 1)
    {

        var uploadedClip = await _context.UploadedClips
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.FileName == filename);

        var movieIdentified = await _context.MoviesIdentified
            .AsNoTracking()
            .Where(m => m.Top_K >= top_k && m.UploadedClipId == uploadedClip.Id)
            .FirstOrDefaultAsync();

        return movieIdentified;
    }

    public async Task<MovieIdentified> InsertMovieIdentifiedAsync(MovieIdentified movieIdentified)
    {
        _context.MoviesIdentified.Add(movieIdentified);

        await _context.SaveChangesAsync();

        return movieIdentified;
    }

    public async Task<MovieIdentified?> UpdateMovieIdentifiedAsync(MovieIdentified movieIdentified)
    {
        var existingMovie = await _context.MoviesIdentified
            .FirstOrDefaultAsync(m => m.Id == movieIdentified.Id);

        if (existingMovie == null)
        {
            return null;
        }

        _context.Entry(existingMovie).CurrentValues.SetValues(movieIdentified);

        await _context.SaveChangesAsync();

        return existingMovie;
    }

    public async Task<MovieIdentified?> GetMovieIdentifiedCountByFileNameAsync(string filename, int top_k)
    {
        var uploaded_clips = await _context.UploadedClips
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.FileName == filename);

        if (uploaded_clips == null)
        {
            return null;
        }

        var movieIdentified = uploaded_clips.MovieIdentifiedId ?? ""; 

        var movieIdentifiedCount = await _context.MoviesIdentified
            .AsNoTracking()
            .Where(m => m.UploadedClipId == uploaded_clips.Id)
            .FirstOrDefaultAsync();

        // var count = movieIdentifiedCount?.Top_K;

        return movieIdentifiedCount;
    }
}
