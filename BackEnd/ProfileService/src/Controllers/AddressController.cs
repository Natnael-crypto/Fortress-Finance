using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Interfaces;
using ProfileService.Models;

namespace ProfileService.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController(IAddressRepository repository) : ControllerBase
{
    private readonly IAddressRepository _repository = repository;

    [HttpPost("CreateAddress")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Address>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> CreateAddressAsync(Address address)
    {
        try
        {
            Address newAddress = await _repository.CreateAddressAsync(address);
            return Results.Created($"Address/{newAddress.UserUuid}",newAddress);
        }
        catch (Exception e)
        {
            ProblemDetails problem =
                new() { Detail = $"An error occured during Address creation: {e.Message}" };
            return Results.BadRequest(problem);
        }
    }

    [HttpGet("{userUuid}/CreateAddress")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Address>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> GetAddressByIdAsync([FromRoute] Guid userUuid)
    {
        try
        {
            Address address = await _repository.GetAddressByIdAsync(userUuid);
            return Results.Ok(address);
        }
        catch (Exception e)
        {
            ProblemDetails problem =
                new() { Detail = $"An error occured while fetching Address: {e.Message}" };
            return Results.BadRequest(problem);
        }
    }

    [HttpPut("UpdateAddress")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Address>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> UpdateAddressAsync(Address address)
    {
        try
        {
            Address updatedAddress = await _repository.UpdateAddressAsync(address);
            return Results.Ok(updatedAddress);
        }
        catch (Exception e)
        {
            ProblemDetails problem =
                new() { Detail = $"An error occured while updating Address: {e.Message}" };
            return Results.BadRequest(problem);
        }
    }


    [HttpPut("{userUuid}/DeleteProfile")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> DeleteAddressAsync([FromRoute] Guid userUuid)
    {
        try
        {
            await _repository.DeleteAddressAsync(userUuid);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            ProblemDetails problem =
                new() { Detail = $"An error occured while deleting Address: {e.Message}" };
            return Results.BadRequest(problem);
        }
    }
}
