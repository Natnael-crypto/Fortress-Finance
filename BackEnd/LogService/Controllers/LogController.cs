using LogService.Model;
using LogService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogService.Controller;

[ApiController]
[Route("api/[controller]")]
public class LogController: ControllerBase{


    private readonly LogServices _logservices;

    public LogController(LogServices logServices)
    {
        _logservices = logServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _logservices.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var result = await _logservices.GetOne(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Log model)
    {
        var result = await _logservices.Create(model);
        return Ok(result);
    }
}