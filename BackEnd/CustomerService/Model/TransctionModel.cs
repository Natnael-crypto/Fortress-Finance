namespace CustomerService.Model;

public class Transaction{



public int ID { get; set; }

public int AccountNumber { get; set; }

public double TransactionAmount { get; set; }


public string TransactionDateTime { get; set; }

public int ReceiverAccount { get; set; }


}