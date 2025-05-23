public interface IUploadClipService
{
    Task<UploadedClip> UploadClipAsync(IFormFile ClipFile);
    Task<UploadedClip> GetClipByFileNameAsync(string FileName);
    // Task<UploadedClip> GetClipwithFileNameAsync(string FileName);
}