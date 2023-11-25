namespace CustomerService.Model;

public class Transaction{



public int ID { get; set; }

public string AcountNumber { get; set; }

public Double TrasctionAmount { get; set; }

public bool TransctionType { get; set; } // true= credited

public DateTime TransctionDate { get; set; }

public DateTime TransctionTime { get; set; }

public string ReciverAcount { get; set; }

public string Transction_ID { get; set; }

}