public class MovieIdentified
{

    public string Id { get; set; } = string.Empty;
    public string ImdbId { get; set; } = string.Empty;
    public float SimilarityScore { get; set; } = 0f;
    public string MetadataPayload { get; set; } = string.Empty;
    public string UploadedClipId { get; set; } = string.Empty;
    public UploadedClip UploadedClip { get; set; } = null!;

}
