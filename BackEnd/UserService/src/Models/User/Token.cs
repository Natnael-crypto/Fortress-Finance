using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class Token(string token)
{
    [Required]
    public string JWT { get; set; } = token;
}