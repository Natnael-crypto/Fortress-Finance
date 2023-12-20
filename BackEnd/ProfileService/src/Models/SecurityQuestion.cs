using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models;

public class SecurityQuestion(Guid uuid, Guid userUuid, string question, string answer)
{   
    [Required]
    [Key]
    public Guid Uuid { get; set; } = uuid;

    [Required]
    public Guid UserUuid { get; set; }= userUuid;

    [Required]
    public string Question { get; set; } = question;

    [Required]
    public string Answer { get; set; } = answer;

}