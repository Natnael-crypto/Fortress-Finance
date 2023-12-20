using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProfileService.Data;
using ProfileService.Interfaces;
using ProfileService.Models;
using ProfileService.Models.DTOs;

namespace ProfileService.Repositories;

public class ProfileRepository(ProfileDataContext context) : IProfileRepository
{
    private readonly ProfileDataContext _context = context;

    public async Task<Profile> CreateProfileAsync(Profile newProfile)
    {
        EntityEntry<Profile> profileEntry = await _context.Profiles.AddAsync(newProfile);
        await _context.SaveChangesAsync();
        return profileEntry.Entity;
    }

    public async Task<Profile> GetProfileByIdAsync(Guid profileUuid)
    {
        Profile queryedProfile = await _context.Profiles.SingleAsync(p => p.UserUuid == profileUuid);
        return queryedProfile;
    }

    public async Task<Profile> UpdateProfileAsync(Profile profile)
    { 
        Profile updatedProfile = await _context.Profiles.SingleAsync(p => p.UserUuid == profile.UserUuid);
        updatedProfile.FirstName = profile.FirstName;
        updatedProfile.MiddleName = profile.MiddleName;
        updatedProfile.LastName = profile.LastName;
        updatedProfile.PhoneNumber = profile.PhoneNumber;
        updatedProfile.Email = profile.Email;
        updatedProfile.ProfileImage = profile.ProfileImage;
        updatedProfile.IdCardImage = profile.IdCardImage;
        await _context.SaveChangesAsync();
        return updatedProfile;
    }

    public async Task DeleteProfileAsync(Guid profileUuid)
    {
        Profile queryedProfile = await _context.Profiles.SingleAsync(p => p.UserUuid == profileUuid);
        EntityEntry<Profile> deletedProfile = _context.Profiles.Remove(queryedProfile);
        await _context.SaveChangesAsync();
    }
}
