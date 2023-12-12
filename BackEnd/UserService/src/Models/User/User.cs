using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class User(Guid uuid, string username)
{
    [Required]
    public Guid Uuid { get; } = uuid;

    [Required]
    public string Username { get; set; } = username;

}

