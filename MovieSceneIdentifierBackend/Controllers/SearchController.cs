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
    public async Task<IActionResult> SearchMovie([FromBody] IFormFile VideoClip, int Top_K)
    {

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
        if(clipExists != null)
        {
            // fetch movie previous identifed to the filepath 
        }

        var url = await _uploadClipService.UploadClipAsync(VideoClip);
        if (url == null)
        {
            _logger.LogError("cloudinary url shouldnt be null");
            return BadRequest(new { StatusCode = 99, Status = "Error", Error = "cloudinary url shouldnt be null" });
        }

        var searchResult = await _sceneIdentifierService.FetchMatchedMovieWithScene(VideoClip, url, Top_K);


        return Ok(new { StatusCode = 100, Status = "Success", Data = new { /* search results */ } });
    }
}