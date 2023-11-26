using CustomerService.Model;
using Dapper;
 

namespace CustomerService.Services;

public class CustomerServices{
    private CustomerDbContext _customerDbContext;

    public CustomerServices(CustomerDbContext customerDbContext){
        _customerDbContext=customerDbContext;
    }


    public async Task<Customer> GetBalance(string AccountNumber)
    {
        using (var connection = _customerDbContext.GetConnection())
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Customer>("SELECT AccountNumber,AccountBalance FROM CustomerTable where AccountNumber=@AccountNumber",new{AccountNumber=AccountNumber});
        }
    }

    public async Task<IEnumerable<Customer>> GetAllCustomer()
    {
        using (var connection = _customerDbContext.GetConnection())
        {
            connection.Open();
            return await connection.QueryAsync<Customer>("SELECT * FROM CustomerTable");
        }
    }

    

    
}