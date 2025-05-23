public interface IUploadedClipRepository
{
    Task<UploadedClip> InsertUploadedClipAsync(UploadedClip Clip);
    Task<UploadedClip?> GetClipWithFileNameAsync(string FileName);
}