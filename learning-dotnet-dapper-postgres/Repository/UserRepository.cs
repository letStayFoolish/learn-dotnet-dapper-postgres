using Dapper;
using learning_dotnet_dapper_postgres.Entities;
using learning_dotnet_dapper_postgres.Helpers;
using learning_dotnet_dapper_postgres.Interfaces;

namespace learning_dotnet_dapper_postgres.Repository;

public class UserRepository : IUserRepository
{
  private readonly DataContext _context;

  public UserRepository(DataContext context)
  {
    _context = context;
  }
  public async Task<IEnumerable<User>> GetAllAsync()
  {
    using var connection = _context.CreateConnection();

    var sql = """
              SELECT * FROM Users;
              """;

    return await connection.QueryAsync<User>(sql);
  }

  public async Task<User?> GetUserByIdAsync(int id)
  {
    using var connection = _context.CreateConnection();

    var sql = """
              SELECT * FROM Users 
              WHERE id = @id
              """;

    return await connection.QuerySingleOrDefaultAsync<User>(sql, new { id });
  }

  public async Task<User?> GetUserByEmailAsync(string email)
  {
    var sql = """
              SELECT * FROM Users 
              WHERE email = @email;
              """;
    
    using var connection = _context.CreateConnection();
    return await connection.QuerySingleOrDefaultAsync<User>(sql, new {email});
  }

  public async Task CreateNewUserAsync(User user)
  {
    var connection = _context.CreateConnection();

    var sql = """
              INSERT INTO Users (Title, FirstName, LastName, Email, Role, PasswordHash)
              VALUES (@Title, @FirstName, @LastName, @Email, @Role, @PasswordHash); 
              """;
    
    await connection.ExecuteAsync(sql, user);
  }

  public Task UpdateUserAsync(User user)
  {
    throw new NotImplementedException();
  }

  public Task DeleteUserAsync(int UserId)
  {
    throw new NotImplementedException();
  }
}