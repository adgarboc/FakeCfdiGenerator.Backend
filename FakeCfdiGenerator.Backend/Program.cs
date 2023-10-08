using dotenv.net;
using FakeCfdiGenerator.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;
using Serilog;


DotEnv.Load();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Fatal()
    .WriteTo.Console()
    .WriteTo.File("fatal.log")
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Configuration.AddEnvironmentVariables();
    
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.File("logs/logs_.log", rollingInterval: RollingInterval.Day)
        .WriteTo.Console()
        .CreateLogger();
    builder.Host.UseSerilog();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<ApplicationContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection") ??
                             throw new ArgumentException("SqlConnection is null"));
    });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "AllowOrigin",
            corsPolicyBuilder =>
            {
                corsPolicyBuilder.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ??
                                              throw new ArgumentException("AllowedOrigins is null"))
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });

    var app = builder.Build();

// Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowOrigin");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    var urls = builder.Configuration["ASPNETCORE_URLS"]?.Split(";");
    if (urls != null)
    {
        foreach (var url in urls)
        {
            Console.WriteLine("Application running on: {0}/swagger/", url);
        }
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}