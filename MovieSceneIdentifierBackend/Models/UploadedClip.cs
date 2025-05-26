public class UploadedClip
{
    public string Id { get; set; } = string.Empty;
    public string CloudinaryFilePath { get; set; } = string.Empty;
    public string CloudinaryFileName { get; set; } = string.Empty;
    public string CloudinaryFileType { get; set; } = string.Empty;
    public string CloudinaryFileSize { get; set; } = string.Empty;
    public string CloudinaryPublicId { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public long FileSize { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string MovieIdentifiedId { get; set; } = string.Empty;
    public MovieIdentified MovieIdentified { get; set; } = null!;
}