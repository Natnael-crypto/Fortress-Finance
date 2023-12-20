using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models.DTOs;

public class SecurityQuestionDTO(Guid userUuid, string question, string answer)
{

    [Required]
    public Guid UserUuid { get; set; } = userUuid;

    [Required]
    public string Question { get; set; } = question;

    [Required]
    public string Answer { get; set; } = answer;

}