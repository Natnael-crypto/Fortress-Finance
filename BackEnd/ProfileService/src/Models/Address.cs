using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models;

public class Address
(Guid uuid, Guid userUuid, string country, string region, string city, string subCity, int woreda, string houseNumber)
{
    [Required]
    [Key]
    public Guid Uuid { get; set; } = uuid;

    [Required]
    public Guid UserUuid { get; set; }  = userUuid;

    [Required]
    public string Country { get; set; } = country;

    [Required]
    public string Region { get; set; } = region;

    [Required]
    public string City { get; set; } = city;

    [Required]
    public string SubCity { get; set; } = subCity;

    [Required]
    public int Woreda { get; set; } = woreda;

    [Required]
    public string HouseNumber { get; set; } = houseNumber;
}