using BCrypt.Net;
using MySqlConnector;
using UserService.Models;

namespace UserService.Repositories;

public class UserRepository
{

    private readonly IConfiguration _configuration;
    private readonly MySqlConnection _connection;

    public UserRepository(IConfiguration configuration)
    {
         _configuration = configuration;
         _connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
    }

    public async Task<User> CreateUserAsync(User user)
    {
        try
        {
            await _connection.OpenAsync();
            // This is the vulnerable version of the application, so SQL statements are intentionally unparameterized
            using var initialCommand = new MySqlCommand("CREATE TABLE IF NOT EXISTS user(id int PRIMARY KEY AUTO_INCREMENT, uuid varchar(130) NOT NULL, username varchar(30) NOT NULL, password varchar(30) NOT NULL, customer BOOL DEFAULT 1, admin BOOL DEFAULT 0);", _connection);
            await initialCommand.ExecuteNonQueryAsync();

            using var checkUserCommand = new MySqlCommand($"SELECT username from user where username = '{user.Username}';", _connection);
            var userExistence = await checkUserCommand.ExecuteReaderAsync();

            // throw an exception if the username already exists in the database.
            if (userExistence.HasRows)
            {
                throw new UserExistsException();
            }
            userExistence.Close();

            Guid newUuid = Guid.NewGuid();
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 13);
            using var command = new MySqlCommand($"INSERT INTO user(uuid, username, passwordHash) VALUES ('{newUuid}', '{user.Username}', '{passwordHash}');", _connection);
            int rowsAffected = await command.ExecuteNonQueryAsync();
            return new User(newUuid, user.Username, "");
        }

        finally
        {
            if(_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}