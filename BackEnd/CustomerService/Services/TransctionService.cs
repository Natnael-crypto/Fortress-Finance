
using CustomerService.Model;
using Dapper;




namespace CustomerService.Services;

public class TransactionService{
    private TransactionDbContext _transactionDbContext;

    public TransactionService(TransactionDbContext transactionDbContext){
        _transactionDbContext=transactionDbContext;
    }


    public async Task<IEnumerable<Transaction>> ViewAllTransaction()
    {
        using (var connection = _transactionDbContext.GetConnection())
        {
            connection.Open();
            return await connection.QueryAsync<Transaction>("SELECT * FROM TransactionTable");
        }
    }

    public async Task<Transaction> MakeTransaction(int CreditedAccountNumber,int DebitedAccountNumber,Double Amount){
        using (var connection = _transactionDbContext.GetConnection())
        {
            connection.Open();
            Customer DebitedCustomer=await connection.QueryFirstOrDefaultAsync<Customer>("SELECT AccountBalance FROM CustomerTable Where AccountNumber=@AccountNumber",new{AccountNumber=DebitedAccountNumber});

            if (DebitedCustomer.AccountBalance<(Amount+Amount*.02)){
                return null;
            }
            Transaction transaction=new Transaction(){
                AccountNumber=DebitedAccountNumber,
                TransactionAmount=Amount,
                TransactionDateTime=DateTime.UtcNow.ToString(),
                ReceiverAccount=CreditedAccountNumber,
            };

            
            Customer CreditedCustomer=await connection.QueryFirstOrDefaultAsync<Customer>("SELECT AccountBalance FROM CustomerTable Where AccountNumber=@AccountNumber",new{AccountNumber=CreditedAccountNumber});

        
            await connection.ExecuteAsync("UPDATE CustomerTable SET AccountBalance = @Amount WHERE AccountNumber = @AccountNumber" ,new {Amount=DebitedCustomer.AccountBalance-Amount,AccountNumber = DebitedAccountNumber});


            await connection.ExecuteAsync("UPDATE CustomerTable SET AccountBalance = @Amount WHERE AccountNumber = @AccountNumber" ,new {Amount=CreditedCustomer.AccountBalance+Amount,AccountNumber = CreditedAccountNumber});

            

            await connection.ExecuteAsync("INSERT INTO TransactionTable (AccountNumber, TransactionAmount, TransactionDateTime, ReceiverAccount) " +
                               "VALUES (@AccountNumber, @TransactionAmount, @TransactionDateTime, @ReceiverAccount)",
                               transaction);
            

            Transaction insertedTransaction = await connection.QueryFirstOrDefaultAsync<Transaction>(
            "SELECT * FROM TransactionTable WHERE TransactionDateTime=@TransactionDateTime and AccountNumber=@AccountNumber",new{TransactionDateTime=transaction.TransactionDateTime,AccountNumber=transaction.AccountNumber});

            return insertedTransaction;
            
        }
    }

    public async Task<IEnumerable<Transaction>> ViewTransaction(int SenderAccountNumber,int ReceiverAccountNumber)
    {
        using (var connection = _transactionDbContext.GetConnection())
        {
            connection.Open();
            return await connection.QueryAsync<Transaction>("SELECT * FROM TransactionTable Where AccountNumber=@AccountNumber and ReceiverAccount=@ReceiverAccount" ,new {ReceiverAccount=ReceiverAccountNumber,AccountNumber=SenderAccountNumber});
        }
    }
}
