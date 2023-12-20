using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProfileService.Data;
using ProfileService.Interfaces;
using ProfileService.Models;
using ProfileService.Models.DTOs;

namespace ProfileService.Repositories;

public class AddressRepository(ProfileDataContext context) : IAddressRepository
{
    private readonly ProfileDataContext _context = context;

    public async Task<Address> CreateAddressAsync(Address newAddress)
    {
        EntityEntry<Address> addressEntry = await _context.Addresses.AddAsync(newAddress);
        await _context.SaveChangesAsync();
        return addressEntry.Entity;
    }

    public async Task<Address> GetAddressByIdAsync(Guid userUuid)
    {
        Address queryedAddress = await _context.Addresses.SingleAsync(a => a.UserUuid == userUuid);
        return queryedAddress;
    }

    public async Task<Address> UpdateAddressAsync(Address address)
    {
        Address updatedAddress = await _context.Addresses.SingleAsync(
            a => a.UserUuid == address.UserUuid
        );
        updatedAddress.Country = address.Country;
        updatedAddress.Region = address.Region;
        updatedAddress.City = address.City;
        updatedAddress.SubCity = address.SubCity;
        updatedAddress.Woreda = address.Woreda;
        updatedAddress.HouseNumber = address.HouseNumber;

        await _context.SaveChangesAsync();
        return updatedAddress;
    }

    public async Task DeleteAddressAsync(Guid userUuid)
    {
        Address queryedAddress = await _context.Addresses.SingleAsync(a => a.UserUuid == userUuid);
        _context.Addresses.Remove(queryedAddress);
        await _context.SaveChangesAsync();
    }
}
