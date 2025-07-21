using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace learning_dotnet_dapper_postgres.Helpers;

public class DataContext
{
  private readonly DbSettings _dbSettings;

  public DataContext(IOptions<DbSettings> dbSettings)
  {
    _dbSettings = dbSettings.Value;
  }

  public IDbConnection CreateConnection()
  {
    var connectionString = $"Host={_dbSettings.Server}; Database={_dbSettings.Database}; Username={_dbSettings.UserId}; Password={_dbSettings.Password};";
    return new NpgsqlConnection(connectionString);
  }

  public async Task Init()
  {
    await _initDatabase();
    await _initTables();
  }

  private async Task _initDatabase()
  {
    // create database if it doesn't exist
    var connectionString = $"Host={_dbSettings.Server}; Database=postgres; Username={_dbSettings.UserId}; Password={_dbSettings.Password};";
    await using var connection = new NpgsqlConnection(connectionString);
    var sqlDbCount = "SELECT COUNT(*) FROM pg_database WHERE datname = @dbName;";
    var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount, new { dbName = _dbSettings.Database });
    if (dbCount == 0)
    {
      var sql = $"CREATE DATABASE {_dbSettings.Database};";
      await connection.ExecuteAsync(sql);
    }
  }

  private async Task _initTables()
  {
    // create tables if they don't exist
    // The `using` statement ensures that the database connection is properly disposed of when it is no longer needed.
    
    await InitUsers();

    async Task InitUsers()
    {
      using var connection = CreateConnection();
      // Role int => (1 = Admin, 2 = User)
      var sql = """
                CREATE TABLE IF NOT EXISTS Users (
                    Id SERIAL PRIMARY KEY,
                    Title VARCHAR(100),
                    FirstName VARCHAR(100),
                    LastName VARCHAR(100),
                    Email VARCHAR(100) UNIQUE,
                    Role INTEGER,
                    PasswordHash VARCHAR(100)
                    )
                """;
      await connection.ExecuteAsync(sql);
    }
  }
}