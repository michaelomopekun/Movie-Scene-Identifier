public interface IUploadClipService
{
    Task<UploadedClip> UploadClipAsync(IFormFile ClipFile, IEnumerable<MoviePredictionResult> MovieIdentifieds);
    Task<UploadedClip> GetClipByFileNameAsync(string FileName);


    // Task<UploadedClip> GetClipwithFileNameAsync(string FileName);
}