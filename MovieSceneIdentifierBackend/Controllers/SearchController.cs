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
        try
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
                return BadRequest(new { StatusCode = 99, Status = "Error", Error = "numbers of Top similar movies must be greater than 0" });
            }

            // check if filename exist before calling Search endpoint
            var clipExists = await _uploadClipService.GetClipByFileNameAsync(VideoClip.FileName);

            var matchedMoviesCount = await _sceneIdentifierService.GetMoviesIdentifiedCountAsync(VideoClip.FileName, Top_K) ?? null;

            if (clipExists != null && matchedMoviesCount?.Top_K >= Top_K)
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
            else if (clipExists != null && Top_K == 0)
            {
                _logger.LogWarning("Clip exists but Top_K is less than requested: {TopK}", Top_K);
                return BadRequest(new { StatusCode = 99, Status = "Error", Error = "Clip exists but Top_K is less than requested" });
            }

            if (clipExists != null && Top_K > matchedMoviesCount?.Top_K)
            {
                await _sceneIdentifierService.DeleteMovieIdentifiedAsync(matchedMoviesCount.Id);
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
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Application error occurred while searching for movies");
            return StatusCode(500, new { StatusCode = 99, Status = "Error", Error = $"{ex.Message}, possible reasons: the python service is currently down or unreachable" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while searching for movies");
            return StatusCode(500, new { StatusCode = 99, Status = "Error", Error = "An error occurred while searching for movies" });
        }


    }
}