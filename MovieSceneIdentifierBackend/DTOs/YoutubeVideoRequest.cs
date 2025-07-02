using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieSceneIdentifierBackend.DTOs;

public class YoutubeVideoRequest
{
    [Required]
    public string VideoUrl { get; set; } = null!;
    [DefaultValue(3)]
    public int Top_K { get; set; } = 3;
}
