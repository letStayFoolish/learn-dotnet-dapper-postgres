using System.ComponentModel.DataAnnotations;
using learning_dotnet_dapper_postgres.Entities;

namespace learning_dotnet_dapper_postgres.Models;

public class CreateRequest
{
  [Required]
  public string? Title { get; set; }

  [Required]
  public string? FirstName { get; set; }

  [Required]
  public string? LastName { get; set; }

  [Required]
  [EnumDataType(typeof(Role))]
  public string? Role { get; set; }

  [Required]
  [EmailAddress]
  public string? Email { get; set; }

  [Required]
  [MinLength(6)]
  public string? Password { get; set; }

  [Required]
  [Compare("Password")]
  public string? ConfirmPassword { get; set; }
}