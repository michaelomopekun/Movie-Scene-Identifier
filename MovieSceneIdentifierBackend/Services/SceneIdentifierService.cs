
using System.Net.Http.Headers;
using System.Text.Json;

public class SceneIdentifierService : ISceneIdentifierService
{

    private readonly HttpClient _httpClient;
    private readonly ILogger<SceneIdentifierService> _logger;
    private readonly IMovieIdentifiedRepository _movieIdentifiedRepository;
    private readonly IUploadedClipRepository _uploadedClipRepository;
    private const int size = 12;
    private const string Idcharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-/<>=!@#$%&/()[]{}|";
    private string MovieMatchServiceURL = "http://127.0.0.1:5000/search";
    private string OMDB_URL = "http://www.omdbapi.com/?apikey={OMDB_APIKEY}&i={imdb_Id}&plot=full";



    public SceneIdentifierService(HttpClient httpClient, ILogger<SceneIdentifierService> logger, IMovieIdentifiedRepository movieIdentifiedRepository)
    {
        _httpClient = httpClient;
        _logger = logger;
        _movieIdentifiedRepository = movieIdentifiedRepository;
    }


    public async Task<IEnumerable<MoviePredictionResult>> FetchMatchedMovieWithScene(IFormFile ClipFile, int topK)
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
            var response = await _httpClient.PostAsync(MovieMatchServiceURL, content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var detailedMovieSearchResult = await FetchMatchedMovieInfo(json, ClipFile);

            return detailedMovieSearchResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to call Python Service");

            throw;
        }
    }


    public async Task<IEnumerable<MoviePredictionResult>> FetchMatchedMovieInfo(string matchedResultPayload, IFormFile ClipFile)
    {

        try
        {
            var predictions = JsonSerializer.Deserialize<List<PythonServiceResponse>>(matchedResultPayload) ?? new();

            var movieIds = predictions.Select(p => p.id).ToList();

            if (movieIds.Count == 0)
                return Enumerable.Empty<MoviePredictionResult>();

            var fetchTasks = movieIds.Select(id => FetchMovieDetailsFromOMDB(id)).ToList();

            await Task.WhenAll(fetchTasks);

            var results = new List<MoviePredictionResult>();

            for (int i = 0; i < movieIds.Count; i++)
            {
                var imdbId = movieIds[i];

                var json = fetchTasks[i].Result;

                if (string.IsNullOrEmpty(json))
                {
                    _logger.LogWarning("Movie details for IMDB ID {ImdbId} is empty or null", imdbId);
                    continue;
                }

                var movieInfo = JsonSerializer.Deserialize<MoviePredictionResult>(json);
                if (movieInfo == null)
                {
                    _logger.LogWarning("Failed to deserialize movie details for IMDB ID {ImdbId}", imdbId);
                    continue;
                }

                var similarity = 1f - (predictions.FirstOrDefault(p => p.id == imdbId)?.distance ?? 1f);

                movieInfo.Confidence = similarity * 100;

                results.Add(movieInfo);

                var moviePredictedId = Nanoid.Nanoid.Generate(Idcharacters, size);

                var UploadedClipCheck = await _uploadedClipRepository.GetClipWithFileNameAsync(ClipFile.FileName);
                if (UploadedClipCheck != null)
                {
                    moviePredictedId = UploadedClipCheck.MovieIdentifiedId;
                }

                var inserted = await InsertMovieIdentifiedAsync(movieInfo, moviePredictedId);

                movieInfo.MovieIdentifiedId = inserted.Id;

                _logger.LogInformation("Inserted movie identified with ID: {Id} for IMDB ID: {ImdbId}", inserted.Id, imdbId);
            }

            return results;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to parse matched result omdb payload");

            throw;
        }

        throw new NotImplementedException();
    }


    


    public async Task<string> FetchMovieDetailsFromOMDB(string imdbId)
    {
        try
        {
            var OmdbApi = Environment.GetEnvironmentVariable("OMDB_APIKEY");

            var response = await _httpClient.GetAsync(OMDB_URL.Replace("{OMDB_APIKEY}", OmdbApi).Replace("{imdb_Id}", imdbId));

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return json;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch movie details from OMDB API for IMDB ID: {ImdbId}", imdbId);
            return string.Empty;
        }
    }

    public async Task<MovieIdentified> InsertMovieIdentifiedAsync(MoviePredictionResult movieInfo, string moviePredictedId)
    {
        var movieSearchResults = new MovieIdentified
        {
            Id = moviePredictedId,
            ImdbId = movieInfo.ImdbId,
            Confidence = movieInfo.Confidence,
            Title = movieInfo.Title,
            Released = movieInfo.Year,
            Genre = movieInfo.Genre,
            Director = movieInfo.Director,
            Actors = movieInfo.Actors,
            Plot = movieInfo.Plot,
            Language = movieInfo.Language,
            Country = movieInfo.Country,
            Poster = movieInfo.Poster,
            imdbRating = movieInfo.imdbRating,
            Type = movieInfo.Type,
            UploadedClipId = string.Empty,
            Runtime = movieInfo.Runtime,
            Year = movieInfo.Year

        };

        await _movieIdentifiedRepository.InsertMovieIdentifiedAsync(movieSearchResults);
        
        return movieSearchResults;
    }

    public async Task<IEnumerable<MoviePredictionResult>?> GetMovieIdentifiedByFileNameAsync(string filename, int topK = 1)
    {
        var movieIdentified = await _movieIdentifiedRepository.GetMovieIdentifiedByFileNameAsync(filename, topK);
        if (movieIdentified == null)
        {
            _logger.LogWarning("No movie identified found for filename: {Filename}", filename);
            return null;
        }

        var movies = new List<MoviePredictionResult>();

        foreach (var movie in movieIdentified)
        {

            var moviePredictionResult = new MoviePredictionResult
            {
                ImdbId = movie.ImdbId,
                Title = movie.Title,
                Confidence = movie.Confidence,
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
                MovieIdentifiedId = movie.Id
            };

            movies.Add(moviePredictionResult);
        }
            _logger.LogInformation("Retrieved movie identified for filename: {Filename}", filename);

        return movies;
    }

    public Task<int?> GetMoviesIdentifiedCountAsync(string filename)
    {
        return _movieIdentifiedRepository.GetMovieIdentifiedCountByFileNameAsync(filename);
    }
}