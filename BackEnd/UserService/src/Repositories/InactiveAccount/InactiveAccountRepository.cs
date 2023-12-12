using MySqlConnector;
using UserService.Models;

namespace UserService.Repositories;

public class InactiveAccountRepository{
    private readonly IConfiguration _configuration;
    private readonly MySqlConnection _connection;
    
    public InactiveAccountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
        InitializeAsync().Wait();
    }

    private async Task InitializeAsync()
    {
        try
        {
            await _connection.OpenAsync();
            // This is the vulnerable version of the application, so SQL statements are intentionally unparameterized
            using var initialCommand = new MySqlCommand
            (
                "CREATE TABLE IF NOT EXISTS inactive_accounts(id int PRIMARY KEY AUTO_INCREMENT, account_number int NOT NULL, reason text NOT NULL, deactivated_by int not null);", 
                _connection
            );
            await initialCommand.ExecuteNonQueryAsync();
        }
        catch(Exception e)
        {
            Console.Error.WriteLine($"An error occured during inactive_accounts table initialization: {e.Message}");
        }
        finally
        {
            if(_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }

    public async Task<InactiveAccount> DeactivateAccountAsync(InactiveAccount inactiveAccount)
    {
        try
        {
            await _connection.OpenAsync();
            using var checkInactiveCommand = new MySqlCommand
            (
                $"Select account_number from inactive_accounts where account_number = {inactiveAccount.AccountNumber};",
                _connection
            );
            var checkInactiveReader = await checkInactiveCommand.ExecuteReaderAsync();
            if (checkInactiveReader.HasRows)
            {
                throw new InactiveAccountExistsException();
            }
            checkInactiveReader.Close();

            using var deactivateCommand = new MySqlCommand
            (
                $"Insert INTO inactive_accounts(account_number, reason, deactivated_by) VALUES ({inactiveAccount.AccountNumber}, '{inactiveAccount.Reason}', {inactiveAccount.DeactivatedBy});",
                _connection
            );
            int rowsAffected = await deactivateCommand.ExecuteNonQueryAsync();
            
            using var getDeactivatedCommand = new MySqlCommand
            (
                $"SELECT account_number FROM inactive_accounts where account_number = {inactiveAccount.AccountNumber};",
                _connection
            );
            var accountReader = await getDeactivatedCommand.ExecuteReaderAsync();
            if (accountReader.HasRows)
            {
                return inactiveAccount;
            }

            throw new Exception();
            
        }
        finally
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
    
}