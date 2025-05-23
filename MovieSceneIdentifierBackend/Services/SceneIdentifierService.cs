
using System.Net.Http.Headers;
using System.Text.Json;

public class SceneIdentifierService : ISceneIdentifierService
{

    private readonly HttpClient _httpClient;
    private readonly ILogger<SceneIdentifierService> _logger;


    public SceneIdentifierService(HttpClient httpClient, ILogger<SceneIdentifierService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }


    public async Task<IEnumerable<MovieIdentified>> FetchMatchedMovieWithScene(IFormFile ClipFile, int topK)
    {
        using var content = new MultipartFormDataContent();

        await using var stream = ClipFile.OpenReadStream();

        var streamContent = new StreamContent(stream);

        streamContent.Headers.ContentType = new MediaTypeHeaderValue(ClipFile.ContentType);

        content.Add(streamContent, "file", ClipFile.FileName);

        content.Add(new StringContent(topK.ToString()), "top_k");

        try
        {
            var response = await _httpClient.PostAsync("http://127.0.0.1:5000/search", content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<MovieIdentified>>(json) ?? new();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to call Python Service");

            throw;
        }
    }
}