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
    public async Task<int> InActiveCustomer(string AccountNumber)
    {
        using (var connection = _customerDbContext.GetConnection())
        {
            connection.Open();
            var res=await connection.QueryFirstAsync<Customer>("SELECT Status FROM CustomerTable where AccountNumber=@AccountNumber",new{AccountNumber=AccountNumber});
            if (res!=null & res.Status==true){
                await connection.ExecuteAsync("Update CustomerTable set Status=@Status Where AccountNumber=@AccountNumber",new{Status=false,AccountNumber=AccountNumber});
                return 1;
            }
            return 0;
            
        }
    }

    public async Task<int> ActiveCustomer(string AccountNumber)
    {
        using (var connection = _customerDbContext.GetConnection())
        {
            connection.Open();
            var res=await connection.QueryFirstAsync<Customer>("SELECT Status FROM CustomerTable where AccountNumber=@AccountNumber",new{AccountNumber=AccountNumber});
            if (res!=null & res.Status==false){
                await connection.ExecuteAsync("Update CustomerTable set Status=@Status Where AccountNumber=@AccountNumber",new{Status=true,AccountNumber=AccountNumber});
                return 1;
            }
            return 0;
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

    public async Task<int> CreateCustomer(Customer model)
    {
        using (var connection = _customerDbContext.GetConnection())
        {


            connection.Open();
            var result= await connection.QueryAsync<Customer>("select * from CustomerTable where UserName=@UserName",new{UserName=model.UserName});
            if (result.Count()>0){
                return 0;
            }
            return await connection.ExecuteAsync("Insert into CustomerTable (UserName,AccountBalance,CreatedDate,Status,Role) Values (@UserName,@AccountBalance,@CreatedDate,@Status,@Role)",new{UserName=model.UserName,AccountBalance=model.AccountBalance,CreatedDate=DateTime.UtcNow.ToString(),Status=model.Status,Role=model.Role});
        }
    }

    

    

    
}