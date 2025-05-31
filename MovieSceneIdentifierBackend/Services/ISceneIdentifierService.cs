public interface ISceneIdentifierService
{
    Task<IEnumerable<MoviePredictionResult>> FetchMatchedMovieWithScene(IFormFile ClipFile, int topK);
    Task<IEnumerable<MoviePredictionResult>> FetchMatchedMovieInfo(string matchedResultPayload, IFormFile ClipFile);
    Task<string> FetchMovieDetailsFromOMDB(string imdbId);
    Task<MovieIdentified> InsertMovieIdentifiedAsync(IEnumerable<MoviePredictionResult> movieIdentified, string moviePredictedId, string uploadedClipId);
    Task<IEnumerable<MoviePredictionResult>?> GetMovieIdentifiedByFileNameAsync(string filename, int topK = 1);
    Task<int?> GetMoviesIdentifiedCountAsync(string filename);
}