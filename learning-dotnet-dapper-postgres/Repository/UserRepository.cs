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
              SELECT * FROM Users WHERE id = @id
              """;

    return await connection.QuerySingleOrDefaultAsync<User>(sql, new { id });
  }

  public Task<User?> GetUserByEmailAsync(string email)
  {
    throw new NotImplementedException();
  }

  public Task CreateNewUserAsync(User user)
  {
    throw new NotImplementedException();
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