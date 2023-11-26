// ServiceAController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AdminTransactionController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public AdminTransactionController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5133/"); // Replace with the base address of ServiceB
    }

    [HttpGet("Transactions/{SenderAccountNumber}/{ReceiverAccountNumber}")]
    public async Task<IActionResult> ViewTransaction(int SenderAccountNumber,int ReceiverAccountNumber)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/Transaction/{SenderAccountNumber}/{ReceiverAccountNumber}");
            response.EnsureSuccessStatusCode(); // Throw an exception if the response status code is not a success code.

            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error calling ServiceB: {ex.Message}");
        }
    }

    [HttpGet("Customers")]
    public async Task<IActionResult> ViewCustomer()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/Customer");
            response.EnsureSuccessStatusCode(); // Throw an exception if the response status code is not a success code.

            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error calling ServiceB: {ex.Message}");
        }
    }

}