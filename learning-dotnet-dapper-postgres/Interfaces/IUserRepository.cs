using learning_dotnet_dapper_postgres.Entities;

namespace learning_dotnet_dapper_postgres.Interfaces;

public interface IUserRepository
{
  Task<IEnumerable<User>> GetAllAsync();
  Task<User?> GetUserByIdAsync(int id);
  Task<User?> GetUserByEmailAsync(string email);
  Task CreateNewUserAsync(User user);
  Task UpdateUserAsync(User user);
  // Task UpdateUserAsync(int UserId, User user);
  Task DeleteUserAsync(int userId);
}