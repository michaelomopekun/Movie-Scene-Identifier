public class UploadMovieRequest
{
    public IFormFile ClipFile { get; set; } = null!;
    public string? MovieTitleHint { get; set; }
}