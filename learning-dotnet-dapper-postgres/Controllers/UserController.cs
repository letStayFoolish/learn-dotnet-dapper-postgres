using learning_dotnet_dapper_postgres.Interfaces;
using learning_dotnet_dapper_postgres.Models;
using Microsoft.AspNetCore.Mvc;

namespace learning_dotnet_dapper_postgres.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;

  public UserController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var users = await _userService.GetAllAsync();
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(users);
  }

  [HttpGet]
  [Route("{id:int}")]
  public async Task<IActionResult> GetUser([FromRoute] int id)
  {
    var foundUser = await _userService.GetUserByIdAsync(id);
    
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    if (foundUser == null)
    {
      return NotFound();
    }

    return Ok(foundUser);
  }

  [HttpPost]
  public async Task<IActionResult> CreateUser([FromBody] CreateRequest requestModel)
  {
    bool userExists = await _userService.IsUserEmailAlreadyExistAsync(requestModel.Email!);
    if (userExists)
    {
      return Conflict(new { message = $"User with the {requestModel.Email} email already exist!" });
    }

    await _userService.CreateNewUserAsync(requestModel);

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(new { message = "User created successfully" });
  }

  // Update
  [HttpPut]
  [Route("{id:int}")]
  public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateRequest requestModel)
  {
    var userFound = await _userService.GetUserByIdAsync(id);

    if (userFound == null)
    {
      return NotFound();
    }
    
    // check if email already exists in the database
    var isEmailExist = await _userService.IsUserEmailAlreadyExistAsync(requestModel.Email!);

    if (isEmailExist)
    {
      return Conflict(new { message = $"User with the {requestModel.Email!} email already exist!" });
    }

    if (!string.IsNullOrEmpty(requestModel.Password))
    {
      requestModel.Password = BCrypt.Net.BCrypt.HashPassword(requestModel.Password);
    }

    await _userService.UpdateUserAsync(requestModel, userFound);
    
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(new { message = "User updated successfully" });
  }
  
  [HttpDelete]
  [Route("{id:int}")]
  public async Task<IActionResult> DeleteUser([FromRoute] int id)
  {
    var userFound = await _userService.GetUserByIdAsync(id);
    
    if(userFound == null) NotFound();

    await _userService.DeleteUserAsync(id);
    
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(new { message = "User deleted successfully" });
  }
}