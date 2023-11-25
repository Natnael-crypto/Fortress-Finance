using Dapper;


namespace CustomerService.Services;

public class CustomerServices{
    private CustomerDbContext _customerDbContext;

    public CustomerServices(CustomerDbContext customerDbContext){
        _customerDbContext=customerDbContext;
    }

    

}