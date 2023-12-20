using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Interfaces;
using ProfileService.Models;
using ProfileService.Models.DTOs;

namespace ProfileService.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController(IProfileRepository repository) : ControllerBase
{
    private readonly IProfileRepository _repository = repository;

    [HttpPost("CreateProfile")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Profile>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> CreateProfileAsync(Profile profile)
    {
        try
        {
            await _repository.CreateProfileAsync(profile);
            return Results.Created($"Profile/{profile.UserUuid}", profile);
        }
        catch (Exception e)
        {
            ProblemDetails problem =
                new() { Detail = $"An error occurred while creating Profile: {e.Message}" };
            return Results.BadRequest(problem);
        }
    }

    [HttpGet("{userUuid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Profile>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> GetProfileByIdAsync([FromRoute] Guid userUuid)
    {
        try
        {
            Profile queryedProfile = await _repository.GetProfileByIdAsync(userUuid);
            return Results.Ok(queryedProfile);
        }
        catch(Exception e)
        {
            ProblemDetails problem =
                new() { Detail = $"An error occurred while fetching Profile: {e.Message}" };
            return Results.BadRequest(problem);
        }
    }

    [HttpPut("UpdateProfile")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Profile>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> UpdateProfileAsync(Profile profile)
    {
        try
        {
            Profile updatedProfile = await _repository.UpdateProfileAsync(profile);
            return Results.Ok(updatedProfile);
        }
        catch(Exception e)
        {
            ProblemDetails problem = 
                new () { Detail = $"An error occurred while updating the Profile: {e.Message}"};
            return Results.BadRequest(problem);
        }
    }
        
    [HttpDelete("{userUuid}/DeleteProfile")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> DeleteProfileAsync(Guid userUuid)
    {
        try
        {
            await _repository.DeleteProfileAsync(userUuid);
            return Results.NoContent();
        }
        catch(Exception e)
        {
            ProblemDetails problem =
                new() { Detail = $"An error occurred during Profile deletion: {e.Message}" };
            return Results.BadRequest(problem);
        }
    }
}

