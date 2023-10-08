using FakeCfdiGenerator.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options => 
    { options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")); });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();