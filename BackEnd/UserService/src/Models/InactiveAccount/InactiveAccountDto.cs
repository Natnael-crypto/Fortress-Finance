using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class InactiveAccount(int accountNumber, string reason, int deactivatedBy){

    // Customer Account Number
    [Required]
    public int AccountNumber { get; set; } = accountNumber;

    [Required]
    public string Reason { get; set; } = reason;

    // Admin Id
    [Required]
    public int DeactivatedBy { get; set; } = deactivatedBy;
}