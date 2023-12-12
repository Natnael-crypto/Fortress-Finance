namespace UserService.Models;

public class InactiveAccount(int id, int accountNumber, string reason, int deactivatedBy){
    public int Id { get; set; } = id;

    // Customer Account Number
    public int AccountNumber { get; set; } = accountNumber;

    public string Reason { get; set; } = reason;

    // Admin Id
    public int DeactivatedBy { get; set; } = deactivatedBy;
}