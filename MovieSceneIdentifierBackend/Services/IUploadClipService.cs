public interface IUploadClipService
{
    Task<IEnumerable<UploadedClip>> UploadClipAsync(IFormFile ClipFile, IEnumerable<MoviePredictionResult> MovieIdentifieds);
    Task<UploadedClip> GetClipByFileNameAsync(string FileName);


    // Task<UploadedClip> GetClipwithFileNameAsync(string FileName);
}