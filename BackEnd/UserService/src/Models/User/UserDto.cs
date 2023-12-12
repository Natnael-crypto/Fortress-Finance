using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class UserDto(string username, string password){
    
    [Required]
    public string Username { get; set; } = username;
    
    [Required]
    public string Password { get; set; } = password;
}