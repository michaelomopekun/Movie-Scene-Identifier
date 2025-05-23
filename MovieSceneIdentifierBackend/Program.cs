using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure DbContext
var connectionString = $"Server={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
                      $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
                      $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                      $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                      $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};" +
                      $"SSL Mode={Environment.GetEnvironmentVariable("POSTGRES_SSLMODE")};Trust Server Certificate=true";
                      
builder.Services.AddDbContext<MovieIdentifierDbContext>(options =>
    options.UseNpgsql(connectionString,
        x => x.MigrationsAssembly(typeof(MovieIdentifierDbContext).Assembly.FullName)));


// configure cloudinary
builder.Services.AddSingleton(serviceProvider =>
{
    var account = new Account(
        Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME"),
        Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY"),
        Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET"));

    return new Cloudinary(account);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
