// ServiceAController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AdminActivationController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public AdminActivationController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5133"); 
    }

    [HttpGet("Inactive/{AccountNumber}")]
    public async Task<IActionResult> ViewLog(int AccountNumber)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/Customer/Inactive/{AccountNumber}");
            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error calling ServiceB: {ex.Message}");
        }
    }

    [HttpGet("active/{AccountNumber}")]
    public async Task<IActionResult> ViewLogOne(int AccountNumber)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/Customer/active/{AccountNumber}");
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