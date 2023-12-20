using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models;

public class Profile(
    Guid userUuid,
    string firstName,
    string middleName,
    string lastName,
    string phoneNumber,
    string email,
    string profileImage,
    string idCardImage
)
{
    [Required]
    [Key]
    public Guid UserUuid { get; set; } = userUuid;

    [Required]
    public string FirstName { get; set; } = firstName;

    [Required]
    public string MiddleName { get; set; } = middleName;

    [Required]
    public string LastName { get; set; } = lastName;

    [Required]
    public string PhoneNumber { get; set; } = phoneNumber;

    [Required]
    public string Email { get; set; } = email;

    public string ProfileImage { get; set; } = profileImage;

    [Required]
    public string IdCardImage { get; set; } = idCardImage;
}
