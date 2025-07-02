using System.Text.Json;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class UploadClipService : IUploadClipService
{
    private readonly IUploadedClipRepository _uploadedClipRepository;
    private readonly IMovieIdentifiedRepository _movieIdentifiedRepository;
    private readonly Cloudinary _cloudinary;
    private const int size = 12;
    private const string Idcharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-/<>=!@#$%&/()[]{}|";


    public UploadClipService(IUploadedClipRepository uploadedClipRepository, Cloudinary cloudinary, IMovieIdentifiedRepository movieIdentifiedRepository)
    {
        _uploadedClipRepository = uploadedClipRepository;
        _cloudinary = cloudinary;
        _movieIdentifiedRepository = movieIdentifiedRepository;
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

    public async Task<UploadedClip> UploadClipAsync(IFormFile ClipFile, IEnumerable<MoviePredictionResult> MovieIdentifieds, int Top_K)
    {
        await using var stream = ClipFile.OpenReadStream();

        var uploadParams = new VideoUploadParams()
        {
            File = new FileDescription(ClipFile.FileName, stream),
            Folder = "uploaded_clips"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        var pathUrl = uploadResult.SecureUrl.AbsoluteUri;

        var clipId = await Nanoid.Nanoid.GenerateAsync(Idcharacters, size);

        var listMovieEntity = new List<MovieIdentified>();

        var listUploadedClip = new List<UploadedClip>();

        foreach (var movie in MovieIdentifieds)
        {

            var movieEntityPayload = new MovieIdentifiedPayload
            {
                ImdbId = movie.ImdbId,
                Title = movie.Title,
                Year = movie.Year,
                Released = movie.Released,
                Runtime = movie.Runtime,
                Genre = movie.Genre,
                Director = movie.Director,
                Actors = movie.Actors,
                Plot = movie.Plot,
                Language = movie.Language,
                Country = movie.Country,
                Poster = movie.Poster,
                imdbRating = movie.imdbRating,
                Type = movie.Type,
                Confidence = movie.Confidence,
            };

            var movieEntity = new MovieIdentified
            {
                Id = movie.MovieIdentifiedId,
                UploadedClipId = clipId,
                Payload = JsonSerializer.Serialize(movieEntityPayload)
            };

            listMovieEntity.Add(movieEntity);
        }

        var movieEntityPayloadSerialized = JsonSerializer.Serialize<List<MovieIdentified>>(listMovieEntity);

        var movieIdList = MovieIdentifieds.Select(i => i.MovieIdentifiedId).ToList();

        var newListMovieEntity = new MovieIdentified
        {
            Id = movieIdList[0],
            UploadedClipId = clipId,
            Top_K = Top_K,
            Payload = movieEntityPayloadSerialized
        };

        await _movieIdentifiedRepository.InsertMovieIdentifiedAsync(newListMovieEntity);


        var uploadedClip = new UploadedClip
        {

            Id = clipId,
            FileName = ClipFile.FileName,
            FileSize = ClipFile.Length,
            FileType = ClipFile.ContentType,
            CloudinaryFileName = uploadResult.DisplayName,
            CloudinaryFilePath = pathUrl,
            CloudinaryFileType = uploadResult.Format,
            CloudinaryFileSize = uploadResult.Bytes.ToString(),
            CloudinaryPublicId = uploadResult.PublicId,
            CreatedAt = DateTime.UtcNow,
            MovieIdentifiedId = movieIdList[0],

        };


        await _uploadedClipRepository.InsertUploadedClipAsync(uploadedClip);

        return uploadedClip;
    }

        
}

