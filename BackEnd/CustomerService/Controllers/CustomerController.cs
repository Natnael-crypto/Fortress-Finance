using CustomerService.Model;
using CustomerService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controller;

[ApiController]
[Route("api/[controller]")]
public class CustomerController: ControllerBase{
    private readonly CustomerServices _CustomerServices;

    public CustomerController(CustomerServices customerServices)
    {
        _CustomerServices = customerServices;
    }



    [HttpGet("{AccountNumber}")]
    public async Task<IActionResult> GetOne( string AccountNumber)
    {
        var result = await _CustomerServices.GetBalance(AccountNumber);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("Inactive/{AccountNumber}")]
    public async Task<IActionResult> InactiveAccount( string AccountNumber)
    {
        var result = await _CustomerServices.InActiveCustomer(AccountNumber);
        if (result == 0)
        {
            return NotFound($"{AccountNumber} Not Found");
        }
        return Ok($"{AccountNumber} Inactivated");
    }

    [HttpGet("active/{AccountNumber}")]
    public async Task<IActionResult> ActiveAccount( string AccountNumber)
    {
        var result = await _CustomerServices.ActiveCustomer(AccountNumber);
        if (result ==0)
        {
            return NotFound($"{AccountNumber} Not Found");
        }
        return Ok($"{AccountNumber} Activated");
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        var result = await _CustomerServices.GetAllCustomer();
        
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] Customer model)
    {
        var result = await _CustomerServices.CreateCustomer(model);
        return Ok(result);
    }



}