using Microsoft.EntityFrameworkCore;
using TodoApp.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoApp.Repository;
using TodoApp.Utilities;
using Amazon.S3;
using Amazon;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor(); // Add IHttpContextAccessor

// Configuración del servicio de base de datos
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configurar AWS S3 explícitamente
builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var config = builder.Configuration.GetSection("AWS");
    var accessKey = config["AccessKey"];
    var secretKey = config["SecretKey"];
    var region = RegionEndpoint.GetBySystemName(config["Region"]);
    return new AmazonS3Client(accessKey, secretKey, region);
});

// Agregar servicios de controladores, API Explorer y Swagger
builder.Services.AddControllers();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
// builder.Services.AddScoped<IFileStorage, S3FileStorage>();
builder.Services.AddScoped<IFileStorage, LocalFileStorage>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Middleware de CORS
app.UseCors("AllowLocalhost4200");

app.UseAuthorization();

app.MapControllers();

app.Run();