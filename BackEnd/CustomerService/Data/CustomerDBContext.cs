using MySql.Data.MySqlClient;

public class CustomerDbContext
{
    private readonly string _connectionString;

    public CustomerDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}