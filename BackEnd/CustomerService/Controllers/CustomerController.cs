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



}