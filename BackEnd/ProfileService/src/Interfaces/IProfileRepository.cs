using ProfileService.Models;
using ProfileService.Models.DTOs;

namespace ProfileService.Interfaces;

public interface IProfileRepository
{
    public Task<Profile> GetProfileByIdAsync(Guid userUuid);

    public Task<Profile> CreateProfileAsync(Profile newProfile);

    public Task<Profile> UpdateProfileAsync(Profile updatedProfile);

    public Task DeleteProfileAsync(Guid userUuid);
}
