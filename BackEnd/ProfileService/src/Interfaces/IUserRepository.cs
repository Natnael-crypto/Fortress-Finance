namespace ProfileService.Interfaces;

public interface IUserRepository
{
    public Task<dynamic> GetUserByIdAsync();
}