public class MovieIdentified
{
    public string Id { get; set; } = string.Empty;
    public string UploadedClipId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string MovieId { get; set; } = string.Empty;
    public float Confidence { get; set; } = 0f;
    public string ImdbId { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string TrailerUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ReleaseDate { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Runtime { get; set; } = string.Empty;
    public string Rating { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public UploadedClip UploadedClip { get; set; } = new UploadedClip();
}
