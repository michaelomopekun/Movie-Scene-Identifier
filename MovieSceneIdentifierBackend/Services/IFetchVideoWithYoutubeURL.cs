namespace MovieSceneIdentifierBackend.Services;

public interface IFetchVideoWithYoutubeURL
{
    Task<string> DownloadVideoWithYoutubeURL(string youtubeUrl);
}

