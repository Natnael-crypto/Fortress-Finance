using CustomerService.Model;
using Dapper;
 

namespace CustomerService.Services;

public class CustomerServices{
    private CustomerDbContext _customerDbContext;

    public CustomerServices(CustomerDbContext customerDbContext){
        _customerDbContext=customerDbContext;
    }


    public async Task<Customer> GetBalance(int AccountNumber)
    {
        using (var connection = _customerDbContext.GetConnection())
        {
            connection.Open();

            Customer customer=await connection.QueryFirstOrDefaultAsync<Customer>("SELECT AccountNumber,AccountBalance FROM CustomerTable where AccountNumber=@AccountNumber",new{AccountNumber=AccountNumber});
            
            if (customer.UserName==""){
                return null;
            }
           
            return customer;
        }
    }

    public async Task<int> InActiveCustomer(int AccountNumber)
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


    public async Task<int> ActiveCustomer(int AccountNumber)
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

    public async Task<string> CreateCustomer(Customer model)
    {
        using (var connection = _customerDbContext.GetConnection())
        {


            connection.Open();
            var result= await connection.QueryAsync<Customer>("select * from CustomerTable where UserName=@UserName",new{UserName=model.UserName});
            if (result.Count()>0){
                return "UserName Exist";
            }

            int res=await connection.ExecuteAsync("Insert into CustomerTable (UserName,AccountBalance,CreatedDate,Status) Values (@UserName,@AccountBalance,@CreatedDate,@Status)",new{UserName=model.UserName,AccountBalance=model.AccountBalance,CreatedDate=DateTime.UtcNow.ToString(),Status=true});
            if (res==0){
                return "Unable To create Customer";
            }
            return "Successfully Created the Customer";
        }
    }
    
}