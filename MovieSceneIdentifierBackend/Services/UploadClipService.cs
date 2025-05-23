
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class UploadClipService : IUploadClipService
{
    private readonly IUploadedClipRepository _uploadedClipRepository;
    private readonly Cloudinary _cloudinary;
    private const int size = 12;
    private const string Idcharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-/<>=!@#$%&/()[]{}|";


    public UploadClipService(IUploadedClipRepository uploadedClipRepository, Cloudinary cloudinary)
    {
        _uploadedClipRepository = uploadedClipRepository;
        _cloudinary = cloudinary;
    }


    public async Task<UploadedClip> GetClipByFileNameAsync(string FileName)
    {
        var clip = await _uploadedClipRepository.GetClipWithFileNameAsync(FileName);

        if (clip == null)
        {
            return null!;
        }

        return clip;
    }

    // public Task<UploadedClip> GetClipWithFileNameAsync(string Filename)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<UploadedClip> UploadClipAsync(IFormFile ClipFile)
    {
        await using var stream = ClipFile.OpenReadStream();

        var uploadParams = new VideoUploadParams()
        {
            File = new FileDescription(ClipFile.FileName, stream),
            Folder = "uploaded_clips"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        var pathUrl = uploadResult.SecureUrl.AbsoluteUri;

        var uploadedClip = new UploadedClip
        {

            Id = await Nanoid.Nanoid.GenerateAsync(Idcharacters, size),
            FileName = ClipFile.FileName,
            FileSize = ClipFile.Length,
            FileType = ClipFile.ContentType,
            CloudinaryFileName = uploadResult.DisplayName,
            CloudinaryFilePath = pathUrl,
            CloudinaryFileType = uploadResult.Format,
            CloudinaryFileSize = uploadResult.Bytes.ToString(),
            CloudinaryPublicId = uploadResult.PublicId,
            CreatedAt = DateTime.UtcNow,

        };

        await _uploadedClipRepository.InsertUploadedClipAsync(uploadedClip);

        return uploadedClip;
    }
}