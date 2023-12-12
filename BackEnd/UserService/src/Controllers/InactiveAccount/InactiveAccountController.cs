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
            return Results.Ok(await _repository.DeactivateAccountAsync(inactiveAccount));
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


}