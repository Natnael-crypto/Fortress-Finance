using ProfileService.Models;
using ProfileService.Models.DTOs;

namespace ProfileService.Interfaces;

public interface IAddressRepository
{
    public Task<Address> CreateAddressAsync(Address newAddress);

    public Task<Address> GetAddressByIdAsync(Guid userUuid);

    public Task<Address> UpdateAddressAsync(Address address);

    public Task DeleteAddressAsync(Guid uuid);
}
