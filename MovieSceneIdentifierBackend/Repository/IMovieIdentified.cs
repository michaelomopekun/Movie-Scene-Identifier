public interface IMovieIdentifiedRepository
{
    Task<MovieIdentified?> GetMovieIdentifiedByFileNameAsync(string filename, int top_k = 1);
    Task<IEnumerable<MovieIdentified>> GetAllMoviesIdentifiedAsync(int pageSize = 100, int pageNumber = 1);
    Task<MovieIdentified> InsertMovieIdentifiedAsync(MovieIdentified movieIdentified);
    Task<MovieIdentified?> UpdateMovieIdentifiedAsync(MovieIdentified movieIdentified);
    Task<int?> GetMovieIdentifiedCountByFileNameAsync(string filename, int top_k);
    // Task<bool> DeleteMovieIdentifiedAsync(string id);
}