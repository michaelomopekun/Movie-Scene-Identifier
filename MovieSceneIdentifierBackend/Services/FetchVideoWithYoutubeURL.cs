
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MovieSceneIdentifierBackend.Services;

public class FetchVideoWithYoutubeURL : IFetchVideoWithYoutubeURL
{
    public async Task<string> DownloadVideoWithYoutubeURL(string youtubeUrl)
    {
        var videoId = GetYoutubeVideoId(youtubeUrl);
        if (string.IsNullOrEmpty(videoId))
            throw new ArgumentException("Invalid YouTube URL: Video ID not found.");

        if (!Directory.Exists("temp"))
            Directory.CreateDirectory("temp");

        if (!File.Exists(Path.Combine("temp", $"{videoId}.mp4")))
        {
            var outputPath = Path.Combine("temp", $"{videoId}.mp4");

            var psi = new ProcessStartInfo
            {
                FileName = Environment.GetEnvironmentVariable("YTDLP_PATH") ?? "yt-dlp",
                Arguments = $"-f \"bestvideo[height<=360][ext=mp4]\" -o \"{outputPath}\" \"{youtubeUrl}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                throw new Exception($"yt-dlp failed: {error}");

            return outputPath;
        }

        return Path.Combine("temp", $"{videoId}.mp4");
    }
    
    public string GetYoutubeVideoId(string url)
    {
        var uri = new Uri(url);

        if (uri.Host.Contains("youtu.be"))
        {
            return uri.Segments.Last();
        }

        if (uri.AbsolutePath.StartsWith("/shorts/"))
        {
            return uri.Segments.Last().Trim('/');
        }

        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

        return query["v"] ?? throw new ArgumentException("Invalid YouTube URL: Video ID not found.");
    }

}
