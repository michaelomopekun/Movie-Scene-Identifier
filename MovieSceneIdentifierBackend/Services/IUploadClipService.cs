public interface IUploadClipService
{
    Task<UploadedClip> UploadClipAsync(IFormFile ClipFile, IEnumerable<MoviePredictionResult> MovieIdentifieds, int Top_K);
    Task<UploadedClip> GetClipByFileNameAsync(string FileName);


    // Task<UploadedClip> GetClipwithFileNameAsync(string FileName);
}