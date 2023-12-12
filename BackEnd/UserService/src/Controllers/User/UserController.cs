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

    [HttpPost("Signup")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<User>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> SignupAsync(UserDto user){
        try
        {
            User newUser = await _repository.CreateUserAsync(user);
            return Results.Created($"User/{newUser.Uuid}", newUser);
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

    [HttpPost("Login")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Token>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> LoginAsync(UserDto user){
        try
        {
            string token = await _repository.LoginAsync(user);
            Token jwt = new(token);
            return Results.Ok(jwt);
        }
        catch(Exception e)
        {
            ProblemDetails problem = new();
            if(e is LoginException){
                problem.Detail = "Invalid Login Credentials";
                return Results.BadRequest(problem);
            }
            problem.Detail = $"An error occured during user login: {e.Message}";
            return Results.BadRequest(problem);
        }
    }

    [HttpPost("Logout")]
    public void Logout(User user)
    {
        
    }
}