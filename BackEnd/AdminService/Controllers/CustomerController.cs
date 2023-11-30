namespace AdminService.Controller;

using AdminService.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("api/[controller]")]
public class customerController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public customerController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5133/"); // Replace with the base address of ServiceB
    }

    [HttpGet("transactions")]
    // TODO Add Authorization To Admin
    public async Task<IActionResult> ViewTransaction()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        try
        {
            var response = await _httpClient.GetAsync($"/api/transaction/allTransaction");
            response.EnsureSuccessStatusCode(); // Throw an exception if the response status code is not a success code.

            var content = await response.Content.ReadAsStringAsync();
            var jsonContent = JsonConvert.DeserializeObject<List<Transaction>>(content);
            
            _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5070/api/transaction/allTransaction", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return Ok(jsonContent);
        }
        catch (HttpRequestException ex)
        {
             _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5070/api/transaction/allTransaction", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return StatusCode(500, $"Error calling ServiceB: {ex.Message}");
        }
    }

    [HttpGet("customers")]
    // TODO Add Authorization To Admin
    public async Task<IActionResult> ViewCustomer()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        try
        {
            var response = await _httpClient.GetAsync("/api/customer");
            response.EnsureSuccessStatusCode(); // Throw an exception if the response status code is not a success code.

            var content = await response.Content.ReadAsStringAsync();

            var jsonContent = JsonConvert.DeserializeObject<List<Customer>>(content);

             _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5070/api/customer", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return Ok(jsonContent);
        }
        catch (HttpRequestException ex)
        {
            _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5070/api/customer", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return StatusCode(500, $"Error calling ServiceB: {ex.Message}");
        }
    }

}