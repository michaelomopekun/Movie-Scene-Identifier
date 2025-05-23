using System.ComponentModel.DataAnnotations;

public class UploadMovieRequest
{
    [Required]
    [DataType(DataType.Upload)]
    [Display(Name = "Clip File")]
    [FileExtensions(Extensions = "mp4,mkv", ErrorMessage = "Please upload a valid video file.")]
    [FileSize(20 * 1024 * 1024, 50 * 1024 * 1024, ErrorMessage = "File size must be greater than 20 MB and less than 50 MB.")]
    [RegularExpression(@"^.*\.(mp4|mkv)$", ErrorMessage = "File type is not supported.")]
    [DisplayFormat(ConvertEmptyStringToNull = true)]
    public IFormFile ClipFile { get; set; } = null!;

}
