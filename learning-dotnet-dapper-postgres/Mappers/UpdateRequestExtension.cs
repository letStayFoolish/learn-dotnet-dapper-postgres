using learning_dotnet_dapper_postgres.Entities;
using learning_dotnet_dapper_postgres.Helpers;
using learning_dotnet_dapper_postgres.Models;

namespace learning_dotnet_dapper_postgres.Mappers;

public static class UpdateRequestExtension
{
  public static User ToUser(this UpdateRequest requestModel)
  {
    return new User
    {
      Title = requestModel.Title,
      FirstName = requestModel.FirstName,
      LastName = requestModel.LastName,
      Email = requestModel.Email,
      Role = (Role)Enum.Parse(typeof(Role), requestModel.Role),
      PasswordHash = HashPasswordExtension.HashPassword(requestModel.Password)
    };
  }
}