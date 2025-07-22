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

  public async Task UpdateUserAsync(UpdateRequest updateModel, User userToUpdate)
  {
    if (!string.IsNullOrEmpty(updateModel.Title))
    {
      userToUpdate.Title = updateModel.Title;
    }
    
    if (!string.IsNullOrEmpty(updateModel.FirstName))
    {
      userToUpdate.FirstName = updateModel.FirstName;
    }
    
    if (!string.IsNullOrEmpty(updateModel.LastName))
    {
      userToUpdate.LastName = updateModel.LastName;
    }
    
    if (!string.IsNullOrEmpty(updateModel.Email))
    {
      userToUpdate.Email = updateModel.Email;
    }

    if (!string.IsNullOrEmpty(updateModel.Role))
    {
      userToUpdate.Role = (Role)Enum.Parse(typeof(Role), updateModel.Role);
    }
    
    if (!string.IsNullOrEmpty(updateModel.Password))
    {
      userToUpdate.PasswordHash = updateModel.Password;
    }
    
    await _userRepository.UpdateUserAsync(userToUpdate);
  }

  public async Task DeleteUserAsync(int userId)
  {
    await _userRepository.DeleteUserAsync(userId);
  }
}