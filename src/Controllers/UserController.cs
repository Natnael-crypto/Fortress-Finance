using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserRepository repository) : ControllerBase
{
    private readonly UserRepository _repository = repository;

    [HttpPost("SignUp")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<User>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> SignUpAsync(User user){
        try
        {
            User new_user = await _repository.CreateUserAsync(user);
            return Results.Created($"User/{new_user.Uuid}", new_user);
        }
        catch(Exception e)
        {   
            ProblemDetails problem = new();
            if(e is UserExistsException)
            {
                problem.Detail = "The username already exists.";
                return Results.BadRequest(problem);
            }
            problem.Detail = $"An error occured during user creation: {e.Message}";
            return Results.BadRequest(problem);
        }
    }
}