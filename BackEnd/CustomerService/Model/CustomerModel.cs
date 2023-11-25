namespace CustomerService.Model;

public class Customer{

    public int ID { get; set; }

    public string AcountNumber { get; set; }

    public double AccountBalace { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool Status { get; set; } /// true=active

    public int Role { get; set; }
}
