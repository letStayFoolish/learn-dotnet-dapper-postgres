using System.Globalization;

namespace learning_dotnet_dapper_postgres.Helpers;

public class AppException : Exception
{
  public AppException() : base() {}
  
  AppException(string message) : base(message) {}

  public AppException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
  {
  }
}