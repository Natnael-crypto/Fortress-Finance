using LogService.Model;
using LogService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogService.Controller;

[ApiController]
[Route("api/[controller]")]
public class logController: ControllerBase{

    private readonly LogServices _logservices;

    public logController(LogServices logServices)
    {
        _logservices = logServices;
    }

    [HttpGet]
    // TODO Add Authorization
    public async Task<IActionResult> GetAll()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();


        _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5135/api/logController/", DateTime.Now.ToString(), "JohnDoe", userAgent);

        var result = await _logservices.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    // TODO Add Authorization
    public async Task<IActionResult> GetOne(int id)
    {
        var result = await _logservices.GetOne(id);

        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();


        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        if (result == null)
        {
            _ = Program.SendPostRequest("ERROR", ipAddress, $"http://localhost:5135/api/logController/{id}", DateTime.Now.ToString(), "JohnDoe", userAgent);
            return NotFound();
        }
        _ = Program.SendPostRequest("INFO", ipAddress, $"http://localhost:5135/api/logController/{id}", DateTime.Now.ToString(), "JohnDoe", userAgent);
        return Ok(result);
    }

    [HttpPost]
    // TODO Add Authorization
    public async Task<IActionResult> Post([FromBody] Log model)
    {
        var result = await _logservices.Create(model);
        return Ok(result);
    }
}