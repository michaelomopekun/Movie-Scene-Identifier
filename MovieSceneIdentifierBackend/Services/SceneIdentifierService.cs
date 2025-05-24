
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

        await using var sourceStream = ClipFile.OpenReadStream();

        using var memoryStream = new MemoryStream();

        await sourceStream.CopyToAsync(memoryStream);

        memoryStream.Position = 0;

        var streamContent = new StreamContent(memoryStream);

        streamContent.Headers.ContentType = new MediaTypeHeaderValue(ClipFile.ContentType);

        content.Add(streamContent, "file", ClipFile.FileName);

        content.Add(new StringContent(topK.ToString()), "top_k");

        try
        {
            var response = await _httpClient.PostAsync("http://127.0.0.1:5000/search", content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var predictions = JsonSerializer.Deserialize<List<PythonServiceResponse>>(json) ?? new();

            var movieSearchResults = predictions.Select(result => new MovieIdentified
            {
                ImdbId = result.id,
                SimilarityScore = 100 - (result.distance * 100),
                MetadataPayload = JsonSerializer.Serialize(new
                {
                    imdbId = result.id,
                    document = result.document,
                    similarityScore = 1 - result.distance
                })
            }).ToList();

            return movieSearchResults;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to call Python Service");

            throw;
        }
    }
}