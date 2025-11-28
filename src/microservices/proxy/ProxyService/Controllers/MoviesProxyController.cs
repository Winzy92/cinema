using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProxyService.Helpers;
using ProxyService.Options;

namespace ProxyService.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesProxyController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ProxyOptions _options;
    private static readonly Random Random = new Random();

    public MoviesProxyController(IHttpClientFactory httpClientFactory, IOptions<ProxyOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }
    
    [HttpGet("debug/options")]
    public IActionResult DebugOptions()
    {
        return Ok(new
        {
            GradualMigration = _options.GradualMigration,
            MoviesMigrationPercent = _options.MoviesMigrationPercent,
            MonolithUrl = _options.MonolithUrl,
            MoviesServiceUrl = _options.MoviesServiceUrl
        });
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? id)
    {
        var serviceUrl = GetTargetServiceUrl();
        var client = _httpClientFactory.CreateClient();

        var url = string.IsNullOrEmpty(id) ? $"{serviceUrl}/api/movies" : $"{serviceUrl}/api/movies?id={id}";
        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        return Content(content, "application/json");
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromBody] object movie)
    {
        var serviceUrl = GetTargetServiceUrl();
        var client = _httpClientFactory.CreateClient();

        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(movie),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await client.PostAsync($"{serviceUrl}/api/movies", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        return StatusCode((int)response.StatusCode, responseBody);
    }
    
    private string GetTargetServiceUrl()
    {
        if (!_options.GradualMigration)
            return _options.MonolithUrl;
        
        return TargetServiceUrlHelper.GetTargetServiceUrl(Convert.ToInt32(_options.MoviesMigrationPercent), _options.MoviesServiceUrl, _options.MonolithUrl);
    }
    
}