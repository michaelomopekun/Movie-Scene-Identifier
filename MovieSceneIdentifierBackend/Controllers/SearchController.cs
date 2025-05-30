using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{

    private readonly ILogger<SearchController> _logger;
    private readonly ISceneIdentifierService _sceneIdentifierService;
    private readonly IUploadClipService _uploadClipService;

    public SearchController(ILogger<SearchController> logger, IUploadClipService uploadClipService, ISceneIdentifierService sceneIdentifierService)
    {
        _logger = logger;
        _uploadClipService = uploadClipService;
        _sceneIdentifierService = sceneIdentifierService;
    }



    [HttpPost]
    [Route("SearchMovie")]
    public async Task<IActionResult> SearchMovie([FromForm] UploadMovieRequest request)
    {
        var VideoClip = request.VideoClip;
        var Top_K = request.Top_K;

        _logger.LogInformation("Video clip: {VideoClip}, Top_K: {Top_K}", VideoClip, Top_K);


        if (VideoClip == null)
        {
            _logger.LogError("Video clip is null");
            return BadRequest(new { StatusCode = 99, Status = "Error", Error = "Video clip is required" });
        }

        if (Top_K <= 0)
        {
            _logger.LogError("Top_K must be greater than 0");
            return BadRequest(new { StatusCode = 99, Status = "Error", Error = "Top_K must be greater than 0" });
        }

        // check if filename exist before calling Search endpoint
        var clipExists = await _uploadClipService.GetClipByFileNameAsync(VideoClip.FileName);

        var matchedMoviesCount = await _sceneIdentifierService.GetMoviesIdentifiedCountAsync(VideoClip.FileName, Top_K) ?? 0;

        if (clipExists != null &&  matchedMoviesCount>=Top_K )
        {
            var movieIdentified = await _sceneIdentifierService.GetMovieIdentifiedByFileNameAsync(VideoClip.FileName, Top_K);
            if (movieIdentified != null)
            {
                _logger.LogInformation("Movie already identified: {MovieIdentified}", movieIdentified);

                return Ok(new { StatusCode = 100, Status = "Success", Data = new { movieIdentified } });
            }
            else
            {

                _logger.LogWarning("Clip exists but no movie identified found for file: {FileName}", VideoClip.FileName);
            }
        }

        

        var searchResult = await _sceneIdentifierService.FetchMatchedMovieWithScene(VideoClip, Top_K);

        // var Clip = new UploadedClip();

        if (clipExists == null)
        {
            var Clip = await _uploadClipService.UploadClipAsync(VideoClip, searchResult, Top_K);

            if (Clip == null)
            {
                _logger.LogError("unabke to save entity clip in db");
            }
        }

        // if(Clip != null)
        //     foreach (var result in searchResult)
        //         {
        //             foreach (var clip in Clip)
        //             {
        //                 await _sceneIdentifierService.InsertMovieIdentifiedAsync(result, result.MovieIdentifiedId, clip.Id);
        //             }
        //         }

        return Ok(new { StatusCode = 100, Status = "Success", Data = new { searchResult } });
    }
}