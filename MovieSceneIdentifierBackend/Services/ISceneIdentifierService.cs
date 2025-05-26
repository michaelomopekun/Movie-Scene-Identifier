public interface ISceneIdentifierService
{
    Task<IEnumerable<MoviePredictionResult>> FetchMatchedMovieWithScene(IFormFile ClipFile, int topK);
    Task<IEnumerable<MoviePredictionResult>> FetchMatchedMovieInfo(string matchedResultPayload);
    Task<string> FetchMovieDetailsFromOMDB(string imdbId);
    Task<MovieIdentified> InsertMovieIdentifiedAsync(MoviePredictionResult movieIdentified, string moviePredictedId);
    Task<IEnumerable<MoviePredictionResult>?> GetMovieIdentifiedByFileNameAsync(string filename, int topK = 1);
}