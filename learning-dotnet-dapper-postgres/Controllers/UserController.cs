using learning_dotnet_dapper_postgres.Entities;
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
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var foundUser = await _userService.GetUserByIdAsync(id);

    if (foundUser == null)
    {
      return NotFound();
    }

    return Ok(foundUser);
  }

  [HttpPost]
  public async Task<IActionResult> CreateUser([FromBody] CreateRequest requestModel)
  {
    await _userService.CreateNewUserAsync(requestModel);
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }
    
    return Ok("Success!");
  }
}