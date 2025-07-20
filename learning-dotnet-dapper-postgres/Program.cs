using System.Text.Json.Serialization;
using learning_dotnet_dapper_postgres.Helpers;
using learning_dotnet_dapper_postgres.Interfaces;
using learning_dotnet_dapper_postgres.Repository;
using learning_dotnet_dapper_postgres.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
  var service = builder.Services;
  var env = builder.Environment;

  service.AddCors();
  service.AddControllers().AddJsonOptions(options =>
  {
    // serialize enums as strings in api responses (e.g. Role)
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params (e.g. User update)
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
  });

  // service.AddAutoMapper();
  // configure strongly typed settings object
  service.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));


  // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
  service.AddOpenApi();

  // Configure DI for application services
  service.AddSingleton<DataContext>();
  service.AddScoped<IUserService, UserService>();
  service.AddScoped<IUserRepository, UserRepository>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

// ensure database and tables exist
{
  using var scope = app.Services.CreateScope();
  var context = scope.ServiceProvider.GetRequiredService<DataContext>();
  await context.Init();
}

// configure HTTP request pipeline
{
  // global cors policy
  app.UseCors(x =>
  {
    x.AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod();
  });
  
  //Error handling using middleware ErrorHandlerMiddleware
  // app.UseMiddleware<ErrorHandlerMiddleware>();
  
  app.MapControllers();
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.Run("http://localhost:4000");