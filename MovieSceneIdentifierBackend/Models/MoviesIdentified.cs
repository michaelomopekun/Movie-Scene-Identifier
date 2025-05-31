public class MovieIdentified
{

    public string Id { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public string UploadedClipId { get; set; } = string.Empty;
    public ICollection<UploadedClip> UploadedClips { get; set; } = new List<UploadedClip>();

}


public class MovieIdentifiedPayload
{
    public string ImdbId { get; set; } = string.Empty;
    public float Confidence { get; set; } = 0f;
    public string Title { get; set; } = string.Empty;
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
}