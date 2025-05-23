public interface ISceneIdentifierService
{
    public Task<IEnumerable<MovieIdentified>> FetchMatchedMovieWithScene(IFormFile ClipFile, int topK);
}