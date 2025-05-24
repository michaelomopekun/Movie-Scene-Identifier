public class MoviePredictionResult
{
    public string Title { get; set; } = string.Empty;
    public float Confidence { get; set; } = 0f;
    public string PosterUrl { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string TrailerUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ReleaseDate { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Runtime { get; set; } = string.Empty;
    public string Rating { get; set; } = string.Empty;

}


public class PythonServiceResponse
{
    public string id { get; set; } = string.Empty;
    public string document { get; set; } = string.Empty;
    public float distance { get; set; } = 0f;
}
