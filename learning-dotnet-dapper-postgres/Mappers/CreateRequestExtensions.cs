using learning_dotnet_dapper_postgres.Entities;
using learning_dotnet_dapper_postgres.Models;

namespace learning_dotnet_dapper_postgres.Mappers;

public static class CreateRequestExtensions
{
  public static User ToUser(this CreateRequest requestModel)
  {
    return new User
    {
      Title = requestModel.Title,
      FirstName = requestModel.FirstName,
      LastName = requestModel.LastName,
      Email = requestModel.Email,
      Role = (Role)Enum.Parse(typeof(Role), requestModel.Role!),
      PasswordHash = HashPassword(requestModel.Password)
    };
  }

  private static string HashPassword(string? password)
  {
    return password ?? string.Empty;
  }
}