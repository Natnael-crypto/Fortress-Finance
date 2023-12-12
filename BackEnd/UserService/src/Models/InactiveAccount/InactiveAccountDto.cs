namespace UserService.Models;

public class InactiveAccountDto(int accountNumber, string reason, int deactivatedBy){

    // Customer Account Number
    public int AccountNumber { get; set; } = accountNumber;

    public string Reason { get; set; } = reason;

    // Admin Id
    public int DeactivatedBy { get; set; } = deactivatedBy;
}