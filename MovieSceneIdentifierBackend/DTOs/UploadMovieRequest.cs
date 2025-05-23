using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

public class UploadMovieRequest
{
    [Required(ErrorMessage = "Video clip is required.")]
    [DataType(DataType.Upload)]
    [Display(Name = "VideoClip")]
    // [FileExtensions(Extensions = ".mp4", ErrorMessage = "Please upload a valid video file.")]
    [FileSize(1 * 1024 * 1024, 50 * 1024 * 1024, ErrorMessage = "File size must be greater than 1 MB and less than 50 MB.")]
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    [SwaggerSchema(Format = "binary")]
    public IFormFile VideoClip { get; set; } = null!;

    public int Top_K { get; set; } = 3;

}
