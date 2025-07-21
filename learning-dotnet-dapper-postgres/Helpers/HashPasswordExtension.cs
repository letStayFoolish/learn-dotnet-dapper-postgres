namespace learning_dotnet_dapper_postgres.Helpers;

public static class HashPasswordExtension
{
  public static string HashPassword(string? password)
  {
    return password ?? string.Empty;
  }
}