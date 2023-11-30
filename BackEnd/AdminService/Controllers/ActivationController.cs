namespace AdminService.Controller;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("api/[controller]")]
public class activationController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public activationController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5133"); 
    }

    [HttpGet("inactive/{AccountNumber}")]
    // TODO Add Authorization To Admin
    public async Task<IActionResult> Inactive(int AccountNumber)

    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        try
        {
            

            var response = await _httpClient.GetAsync($"/api/Customer/inactive/{AccountNumber}");
            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
             

            _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5070/api/activationController/inactive/{AccountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return Ok(content);
        }
        catch (HttpRequestException ex)
        {
            _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5070/api/activationController/inactive/{AccountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return StatusCode(500, $"Error calling ServiceB: {ex.Message}");
        }
    }

    [HttpGet("active/{AccountNumber}")]
    // TODO Add Authorization To Admin
    public async Task<IActionResult> Activate(int AccountNumber)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        try
        {
            var response = await _httpClient.GetAsync($"/api/Customer/active/{AccountNumber}");
            response.EnsureSuccessStatusCode(); 

            var content = await response.Content.ReadAsStringAsync();
             _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5070/api/activationController/active/{AccountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return Ok(content);
        }
        catch (HttpRequestException ex)
        {
            _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5070/api/activationController/inactive/{AccountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return StatusCode(500, $"Error calling ServiceB: {ex.Message}");
        }
    }
}