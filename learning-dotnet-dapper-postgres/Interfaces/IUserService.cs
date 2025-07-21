using learning_dotnet_dapper_postgres.Entities;
using learning_dotnet_dapper_postgres.Models;

namespace learning_dotnet_dapper_postgres.Interfaces;

public interface IUserService
{
  Task<IEnumerable<User>> GetAllAsync();
  Task<User?> GetUserByIdAsync(int id);
  Task<bool> IsUserEmailAlreadyExistAsync(string emailAddress);
  Task CreateNewUserAsync(CreateRequest userModel);
  Task UpdateUserAsync(int UserId, UpdateRequest userModel);
  Task DeleteUserAsync(int UserId);
}