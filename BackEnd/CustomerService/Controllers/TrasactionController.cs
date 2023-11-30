using CustomerService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controller;

[ApiController]
[Route("api/[controller]")]
public class transactionController: ControllerBase{
    private readonly TransactionService _transactionservice;

    public transactionController(TransactionService transactionService)
    {
        _transactionservice = transactionService;
    }

    
    [HttpGet("/{accountNumber}")]
    // TODO Add Authorization TO Customer
    public async Task<IActionResult> ViewTransaction( int accountNumber)
    {

        var result = await _transactionservice.ViewTransaction(accountNumber);

        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        if (result == null)
        {
            _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5133/api/transactionController/{accountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return NotFound();
        }
        _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5133/api/transactionController/{accountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
        return Ok(result);
    }


    [HttpGet("{creditedAccountNumber}/{debitedAccountNumber}/{amount}")]
    // TODO Add Authorization TO Customer
    public async Task<IActionResult> MakeTransaction( int creditedAccountNumber,int debitedAccountNumber,Double amount)
    {
        var result = await _transactionservice.MakeTransaction(creditedAccountNumber,debitedAccountNumber,amount);

        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        if (result == null)
        {
            _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5133/api/transactionController/{creditedAccountNumber}/{debitedAccountNumber}/{amount}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return NotFound();
        }
        _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5133/api/transactionController/{creditedAccountNumber}/{debitedAccountNumber}/{amount}", DateTime.Now.ToString(), "JohnDoe", userAgent);
        return Ok($"Credited Account {creditedAccountNumber} with amount of {amount}");
    }

    [HttpGet("allTransaction")]
    // TODO Add Authorization TO Admin
    public async Task<IActionResult> ViewAllTransaction()
    {
        var result = await _transactionservice.ViewAllTransaction();

        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

        // var requestinfo = HttpContext.Request.
        
        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        if (result == null)
        {
            _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5133/api/transactionController/allTransaction", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return NotFound();
        }
        _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5133/api/transactionController/allTransaction", DateTime.Now.ToString(), "JohnDoe", userAgent);
        return Ok(result);
    }
}
