using MySql.Data.MySqlClient;

public class TransactionDbContext
{
    private readonly string _connectionString;

    public TransactionDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}