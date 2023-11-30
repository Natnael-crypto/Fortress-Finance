namespace AdminService.Model;

public class Customer{

    public required int AccountNumber { get; set; }

    public required string UserName {get; set;}

    public required double AccountBalance { get; set; }

    public required string CreatedDate { get; set; }

    public required bool Status { get; set; } /// true=active

    
}
