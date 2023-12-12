using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
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
        InitializeAsync().Wait();
    }

    private async Task InitializeAsync()
    {
        try
        {
            await _connection.OpenAsync();
            // This is the vulnerable version of the application, so SQL statements are intentionally unparameterized
            using var initialCommand = new MySqlCommand("CREATE TABLE IF NOT EXISTS user(id int PRIMARY KEY AUTO_INCREMENT, uuid varchar(130) NOT NULL, username varchar(30) NOT NULL, password_hash varchar(30) NOT NULL, customer BOOL DEFAULT 1, admin BOOL DEFAULT 0);", _connection);
            await initialCommand.ExecuteNonQueryAsync();
        }
        catch(Exception e)
        {
            Console.Error.WriteLine($"An error occured during user table initialization: {e.Message}");
        }
        finally
        {
            if(_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }

    public async Task<User> CreateUserAsync(UserDto user)
    {
        try
        {
            await _connection.OpenAsync();
            using var checkUserCommand = new MySqlCommand($"SELECT username from user where username = '{user.Username}';", _connection);
            var userReader = await checkUserCommand.ExecuteReaderAsync();

            // throw an exception if the username already exists in the database.
            if (userReader.HasRows)
            {
                throw new UserExistsException();
            }
            userReader.Close();

            Guid newUuid = Guid.NewGuid();
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 13);
            using var command = new MySqlCommand($"INSERT INTO user(uuid, username, password_hash) VALUES ('{newUuid}', '{user.Username}', '{passwordHash}');", _connection);
            int rowsAffected = await command.ExecuteNonQueryAsync();
            return new User(newUuid, user.Username);
        }

        finally
        {
            if(_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }

    public async Task<string> LoginAsync(UserDto user){
        try
        {
            await _connection.OpenAsync();
            using var checkUserCommand = new MySqlCommand($"SELECT * from user where username = '{user.Username}';", _connection);
            var userReader = await checkUserCommand.ExecuteReaderAsync();
            
            if (await userReader.ReadAsync())
            {
                string username = userReader.GetString(userReader.GetOrdinal("username"));
                string passwordHash = userReader.GetString(userReader.GetOrdinal("password_hash"));
                if (BCrypt.Net.BCrypt.EnhancedVerify(user.Password, passwordHash))
                {
                    return GenerateToken(user);
                }
                else
                {
                    throw new LoginException();
                }
            } 
            else
            {
                throw new LoginException();
            }

        }
        finally
        {
            if(_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }

    private string GenerateToken(UserDto user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:key"]!));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Username),
            new(ClaimTypes.Role, "admin")
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(8)),
            Issuer = _configuration["JwtSettings:Issuer"],
            Audience = _configuration["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
}