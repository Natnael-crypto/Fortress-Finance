using CustomerService.Model;
using CustomerService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace CustomerService.Controller;

[ApiController]
[Route("api/[controller]")]
public class customerController: ControllerBase{
    private readonly CustomerServices _CustomerServices;

    public customerController(CustomerServices customerServices)
    {
        _CustomerServices = customerServices;
    }



    [HttpGet("{accountNumber}")]
    // TODO Add Authorization To Customer And Admin
    public async Task<IActionResult> GetOne( int accountNumber)
    {
        var result = await _CustomerServices.GetBalance(accountNumber);

        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();
        
        if (result == null)
        {
            _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5133/api/customerController/{accountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return NotFound();
        }
         _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5133/api/customerController/{accountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
        return Ok(result);
    }

    [HttpGet("inactive/{accountNumber}")]
    // TODO Add Authorization To Admin
    public async Task<IActionResult> InactiveAccount( int accountNumber)
    {
        var result = await _CustomerServices.InActiveCustomer(accountNumber);

        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        if (result == 0)
        {
            _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5133/api/customerController/inactive/{accountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return NotFound($"{accountNumber} Not Found");
        }
        _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5133/api/customerController/inactive/{accountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
        return Ok($"{accountNumber} Inactivated");
    } 

    [HttpGet("active/{accountNumber}")]
    // TODO Add Authorization To Admin
    public async Task<IActionResult> ActiveAccount( int accountNumber)
    {
        var result = await _CustomerServices.ActiveCustomer(accountNumber);

        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        if (result ==0)
        {
             _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5133/api/customerController/active/{accountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return NotFound($"{accountNumber} Not Found");
        }
        _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5133/api/customerController/active/{accountNumber}", DateTime.Now.ToString(), "JohnDoe", userAgent);
        return Ok($"{accountNumber} Activated");
    }

    [HttpGet()]
    // TODO Add Authorization To Admin
    public async Task<IActionResult> GetAll()
    {
        var result = await _CustomerServices.GetAllCustomer();

        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();
        
        if (result == null)
        {
            _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5133/api/customerController/", DateTime.UtcNow.ToString(), "JohnDoe", userAgent);

            return NotFound();
        }
        _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5133/api/customerController/",  DateTime.UtcNow.ToString(), "JohnDoe", userAgent);
        return Ok(result);
    }

    [HttpPost]
    // TODO Add Authorization  
    public async Task<IActionResult> CreateCustomer([FromBody] Customer model)
    {
        var result = await _CustomerServices.CreateCustomer(model);

        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        if (result!="Successfully Created the Customer"){
           _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5133/api/customerController/", DateTime.Now.ToString(), "JohnDoe", userAgent);
           throw new Microsoft.AspNetCore.Http.BadHttpRequestException("Username Exist", StatusCodes.Status400BadRequest);
        }
        _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5133/api/customerController/", DateTime.Now.ToString(), "JohnDoe", userAgent);
        return Ok(result);
    }

}