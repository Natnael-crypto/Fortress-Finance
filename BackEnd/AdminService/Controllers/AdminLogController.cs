// ServiceAController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AdminLogController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public AdminLogController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5135"); 
    }

    [HttpGet]
    public async Task<IActionResult> ViewLog()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/Log");
            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error calling ServiceB: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ViewLogOne(int id)
    {
        try
        {
            Console.WriteLine(id);
            var response = await _httpClient.GetAsync($"/api/Log/{id}");
            response.EnsureSuccessStatusCode(); 

            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error calling ServiceB: {ex.Message}");
        }
    }
}