public class MoviePredictionResult
{

    public string ImdbId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public float Confidence { get; set; } = 0f;
    public string Year { get; set; } = string.Empty;
    public string Released { get; set; } = string.Empty;
    public string Runtime { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Actors { get; set; } = string.Empty;
    public string Plot { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Poster { get; set; } = string.Empty;
    public string imdbRating { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string MovieIdentifiedId { get; set; } = string.Empty;

}


public class PythonServiceResponse
{
    public string id { get; set; } = string.Empty;
    public string document { get; set; } = string.Empty;
    public float distance { get; set; } = 0f;
}
