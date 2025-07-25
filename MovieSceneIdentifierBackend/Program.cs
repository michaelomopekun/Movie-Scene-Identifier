using CloudinaryDotNet;
using DotNetEnv;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MovieSceneIdentifierBackend.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

var cloudName = Environment.GetEnvironmentVariable("CLOUDNAME");

Console.WriteLine("============⌚⌚⌚⌚⌚CloudName: " + cloudName + "============");


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    c.SupportNonNullableReferenceTypes();

    c.MapType<IFormFile>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });
});


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
        Environment.GetEnvironmentVariable("CLOUDNAME"),
        Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY"),
        Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET"));

    return new Cloudinary(account);
});


builder.Services.AddScoped<ISceneIdentifierService, SceneIdentifierService>();
builder.Services.AddScoped<IUploadClipService, UploadClipService>();
builder.Services.AddScoped<IUploadedClipRepository, UploadedClipRepository>();
builder.Services.AddScoped<IMovieIdentifiedRepository, MovieIdentifiedRepository>();
builder.Services.AddScoped<IFetchVideoWithYoutubeURL, FetchVideoWithYoutubeURL>();

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 30 * 1024 * 1024;
});


builder.Services.AddHttpClient<ISceneIdentifierService, SceneIdentifierService>(client =>
{
    client.BaseAddress = new Uri("http://127.0.0.1:8010/");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:8080", "http://127.0.0.1:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


var app = builder.Build();

app.UseCors("AllowFrontend");
Console.WriteLine("===> Using CORS Policy: AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
