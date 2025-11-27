using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProxyService.Helpers;
using ProxyService.Options;

namespace ProxyService.Controllers;

[ApiController]
[Route("api/users")]
public class UsersProxyController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ProxyOptions _options;

    public UsersProxyController(IHttpClientFactory httpClientFactory, IOptions<ProxyOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"{_options.MonolithUrl}/api/users");
        var content = await response.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] object user)
    {
        var client = _httpClientFactory.CreateClient();

        var body = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(user),
            System.Text.Encoding.UTF8,
            "application/json");

        var response = await client.PostAsync($"{_options.MonolithUrl}/api/users", body);
        var responseBody = await response.Content.ReadAsStringAsync();

        return StatusCode((int)response.StatusCode, responseBody);
    }
    
}