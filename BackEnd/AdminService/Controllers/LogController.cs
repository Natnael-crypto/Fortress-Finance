namespace AdminService.Controller;

using AdminService.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("api/[controller]")]
public class logController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public logController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5135"); 
    }

    [HttpGet]
    // TODO Add Authorization To Admin
    public async Task<IActionResult> ViewLog()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        try
        {
            var response = await _httpClient.GetAsync("/api/Log");
            response.EnsureSuccessStatusCode(); 
            var content = await response.Content.ReadAsStringAsync();

            var jsonContent = JsonConvert.DeserializeObject<List<Log>>(content);

            _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5070/api/logController/", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return Ok(jsonContent);
        }
        catch (HttpRequestException ex)
        {
             _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5070/api/logController/", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return StatusCode(500, $"Error calling ServiceB: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    // TODO Add Authorization To Admin
    public async Task<IActionResult> ViewLogOne(int id)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        try
        {
            Console.WriteLine(id);
            var response = await _httpClient.GetAsync($"/api/Log/{id}");
            response.EnsureSuccessStatusCode(); 

            var content = await response.Content.ReadAsStringAsync();

            var jsonContent = JsonConvert.DeserializeObject<Log>(content);
            
             _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5070/api/logController/{id}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return Ok(jsonContent);
        }
        catch (HttpRequestException ex)
        {
            _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5070/api/logController/{id}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return StatusCode(500, $"Error calling ServiceB: {ex.Message}");
        }
    }
}