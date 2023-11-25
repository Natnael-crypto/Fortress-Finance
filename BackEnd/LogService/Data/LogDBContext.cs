using MySql.Data.MySqlClient;

public class LogDbContext
{
    private readonly string _connectionString;

    public LogDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}
