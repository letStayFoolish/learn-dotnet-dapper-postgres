using System.Net;
using learning_dotnet_dapper_postgres.Entities;
using learning_dotnet_dapper_postgres.Helpers;
using learning_dotnet_dapper_postgres.Interfaces;
using learning_dotnet_dapper_postgres.Mappers;
using learning_dotnet_dapper_postgres.Models;
using Microsoft.AspNetCore.Mvc;

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

    if (foundUser == null)
    {
      throw new KeyNotFoundException("User not found");
    }

    return foundUser;
  }

  public async Task<bool> IsUserEmailAlreadyExistAsync(string emailAddress)
  {
    // validation
    var foundUser = await _userRepository.GetUserByEmailAsync(emailAddress);
    
    return foundUser != null; // return true if the user already exists in the database
  }

  public async Task CreateNewUserAsync(CreateRequest requestModel)
  {
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