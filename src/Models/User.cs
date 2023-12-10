using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class User(Guid uuid, string username, string password)
{
    public Guid Uuid { get; } = uuid;

    [Required]
    public string Username { get; set; } = username;

    [Required]
    public string Password { get; set; } = password;
}

