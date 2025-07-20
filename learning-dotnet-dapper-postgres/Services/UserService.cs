using learning_dotnet_dapper_postgres.Entities;
using learning_dotnet_dapper_postgres.Interfaces;
using learning_dotnet_dapper_postgres.Mappers;
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

  public async Task CreateNewUserAsync(CreateRequest requestModel)
  {
    // validation
    var userFound = await _userRepository.GetUserByEmailAsync(requestModel.Email);
    if (userFound != null)
    {
      throw new Exception("Email already exists");
    }
    
    // Mapping
    var userModel = requestModel.ToUser();
    userModel.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userModel.PasswordHash);
    
    await _userRepository.CreateNewUserAsync(userModel);
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