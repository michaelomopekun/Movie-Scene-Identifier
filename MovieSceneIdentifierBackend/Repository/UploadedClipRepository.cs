

using Microsoft.EntityFrameworkCore;

public class UploadedClipRepository : IUploadedClipRepository
{
    private readonly MovieIdentifierDbContext _context;


    public UploadedClipRepository(MovieIdentifierDbContext context)
    {
        _context = context;
    }


    public async Task<UploadedClip?> GetClipWithFileNameAsync(string FileName)
    {
        if (_context.UploadedClips == null)
        {
            return null;
        }

        var filePath = await _context.UploadedClips
            .AsNoTracking()
            .Where(f => f.FileName == FileName)
            .FirstOrDefaultAsync();

        if (filePath == null)
        {
            return null;
        }

        return filePath;
    }

    public async Task<UploadedClip> InsertUploadedClipAsync(UploadedClip Clip)
    {
        await _context.AddAsync(Clip);
        _context.SaveChanges();

        return Clip;
    }
}