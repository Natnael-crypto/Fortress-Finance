namespace CustomerService.Model;

public class Customer{

    public int AccountNumber { get; set; }

    public double AccountBalance { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool Status { get; set; } /// true=active

    public int Role { get; set; }
}
