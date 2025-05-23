public interface ISceneIdentifierService
{
    public Task<IEnumerable<MovieIdentified>> FetchMatchedMovieWithScene(IFormFile ClipFile, string url, int topK);
}