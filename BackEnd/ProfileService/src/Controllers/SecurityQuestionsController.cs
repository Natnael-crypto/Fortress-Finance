using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Interfaces;
using ProfileService.Models;
using ProfileService.Models.DTOs;

namespace ProfileService.Controllers;

[ApiController]
[Route("[controller]")]
public class SecurityQuestionsController(ISecurityQuestionRepository repository) : ControllerBase
{
    private readonly ISecurityQuestionRepository _repository = repository;

    [HttpPost("CreateSecurityQuestion")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<SecurityQuestion>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> CreateSecurityQuestionAsync(SecurityQuestionDTO securityQuestionDTO)
    {
        try
        {
            SecurityQuestion newSecurityQuestion = await _repository.CreateSecurityQuestionAsync(
                securityQuestionDTO
            );
            return Results.Created(
                $"SecurityQuestions/{newSecurityQuestion.UserUuid}",
                newSecurityQuestion
            );
        }
        catch (Exception e)
        {
            ProblemDetails problem =
                new() { Detail = $"An error occured while creating SecurityQuestion: {e.Message}" };
            return Results.BadRequest(problem);
        }
    }

    [HttpGet("{userUUid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<List<SecurityQuestion>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> GetSecurityQuestionsByIdAsync([FromRoute] Guid userUuid)
    {
        try
        {
            List<SecurityQuestion> securityQuestions =
                await _repository.GetSecurityQuestionsByIdAsync(userUuid);
            return Results.Ok(securityQuestions);
        }
        catch (Exception e)
        {
            ProblemDetails problem =
                new()
                {
                    Detail = $"An error occured while fetching SecurityQuestions: {e.Message}"
                };
            return Results.BadRequest(problem);
        }
    }

    [HttpPut("UpdateSecurityQuestions")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<List<SecurityQuestion>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> UpdateSecurityQuestionsAsync(
        List<SecurityQuestion> securityQuestions
    )
    {
        try
        {
            List<SecurityQuestion> updatedSecurityQuestions =
                await _repository.UpdateSecurityQuestionsAsync(securityQuestions);
            return Results.Ok(updatedSecurityQuestions);
        }
        catch (Exception e)
        {
            ProblemDetails problem =
                new()
                {
                    Detail = $"An error occured while updating SecurityQuestions: {e.Message}"
                };
            return Results.BadRequest(problem);
        }
    }

    [HttpDelete("SecurityQuestion/{uuid}/DeleteSecurityQuestion")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> DeleteSecurityQuestionAsync(Guid uuid)
    {
        try
        {
            await _repository.DeleteSecurityQuestionAsync(uuid);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            ProblemDetails problem =
                new()
                {
                    Detail = $"An error occured while deleting SecurityQuestion: {e.Message}"
                };
            return Results.BadRequest(problem);
        }
    }
}
