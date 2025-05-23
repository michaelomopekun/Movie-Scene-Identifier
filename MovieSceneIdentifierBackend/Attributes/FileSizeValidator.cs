using System.ComponentModel.DataAnnotations;

public class FileSizeAttribute : ValidationAttribute
{
    private readonly int _minFileSize;
    public readonly int _maxFileSize;
    
    public FileSizeAttribute(int minFileSize, int maxFileSize)
    {
        _minFileSize = minFileSize;
        _maxFileSize = maxFileSize;
    }


    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null && file.Length < _minFileSize)
        {
            return new ValidationResult(ErrorMessage ?? $"Minimum allowed file size is {_minFileSize} bytes.");
        }
        if (file != null && file.Length > _maxFileSize)
        {
            return new ValidationResult(ErrorMessage ?? $"Maximum allowed file size is {_maxFileSize} bytes.");
        }
        return ValidationResult.Success;
    }
}