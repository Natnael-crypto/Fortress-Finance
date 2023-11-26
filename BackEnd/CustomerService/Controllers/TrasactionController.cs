using CustomerService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controller;

[ApiController]
[Route("api/[controller]")]
public class TransactionController: ControllerBase{
    private readonly TransactionService _transactionservice;

    public TransactionController(TransactionService transactionService)
    {
        _transactionservice = transactionService;
    }



    [HttpGet("{SenderAccountNumber}/{ReceiverAccountNumber}")]
    public async Task<IActionResult> ViewTransaction( int SenderAccountNumber,int ReceiverAccountNumber)
    {
        var result = await _transactionservice.ViewTransaction(SenderAccountNumber,ReceiverAccountNumber);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("{CreditedAccountNumber}/{DebitedAccountNumber}/{Amount}")]
    public async Task<IActionResult> MakeTransaction( int CreditedAccountNumber,int DebitedAccountNumber,Double Amount)
    {
        var result = await _transactionservice.MakeTransaction(CreditedAccountNumber,DebitedAccountNumber,Amount);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }


    


}