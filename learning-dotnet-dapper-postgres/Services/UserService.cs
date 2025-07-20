using learning_dotnet_dapper_postgres.Entities;
using learning_dotnet_dapper_postgres.Interfaces;
using learning_dotnet_dapper_postgres.Models;

namespace learning_dotnet_dapper_postgres.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }
  public async Task<IEnumerable<User>> GetAllAsync()
  {
    var users = await _userRepository.GetAllAsync();
    
    return users;
  }

  public async Task<User?> GetUserByIdAsync(int id)
  {
    var foundUser = await _userRepository.GetUserByIdAsync(id);

    return foundUser;
  }

  public Task CreateNewUserAsync(CreateRequest userModel)
  {
    throw new NotImplementedException();
  }

  public Task UpdateUserAsync(int UserId, UpdateRequest userModel)
  {
    throw new NotImplementedException();
  }

  public Task DeleteUserAsync(int UserId)
  {
    throw new NotImplementedException();
  }
}