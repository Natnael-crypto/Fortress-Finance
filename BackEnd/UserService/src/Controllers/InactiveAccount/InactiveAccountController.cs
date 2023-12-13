using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Controllers;

[Route("Account")]
[ApiController]
public class InactiveAccountController(InactiveAccountRepository repository): ControllerBase
{
    private readonly InactiveAccountRepository _repository = repository;

    [Authorize]
    [HttpPost("DeactivateAccount")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<InactiveAccount>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> DeactivateAccountAsync(InactiveAccount inactiveAccount)
    {
        try
        {
            return Results.Created($"InactiveAccount/{inactiveAccount.AccountNumber}",
                await _repository.DeactivateAccountAsync(inactiveAccount));
        }
        catch(Exception e)
        {
            ProblemDetails problem = new();
            if (e is InactiveAccountExistsException)
            {
                problem.Detail = $"The account is already inactive.";
                return Results.BadRequest(problem);
            }
            problem.Detail = $"An error occured during user login: {e.Message}";
            return Results.BadRequest(problem);
        }
    }

    [Authorize]
    [HttpGet("InactiveAccounts")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<List<InactiveAccount>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> GetInactiveAccountsAsync()
    {
        try
        {
            List<InactiveAccount> inactiveAccounts = await _repository.GetInactiveAccountsAsync();
            return Results.Ok(inactiveAccounts);            
        }
        catch(Exception e)
        {
            ProblemDetails problem = new()
            {
                Detail = $"An error occured while fetching inactive account: {e.Message}"
            };
            return Results.BadRequest(problem);
        }

    }

    [Authorize]
    [HttpDelete("ReactivateAccount")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> ReactivateAccountAsync(InactiveAccount inactiveAccount)
    {
        try
        {
            int rowsAffected = await _repository.ReactivateAccountAsync(inactiveAccount);
            return Results.NoContent();
        }
        catch(Exception e)
        {
            ProblemDetails problem = new();
            if(e is InactiveAccountNotExistException)
            {
                problem.Detail = "The account is not inactive";
                return Results.BadRequest(problem);
            }
            problem.Detail = $"An error occured during account reactivation: {e.Message}";
            return Results.BadRequest(problem);
        }

    }



}