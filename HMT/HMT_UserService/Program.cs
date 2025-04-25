using HMT.Foundation.Validators.Users;
using HMT_UserService.Data;
using HMT_UserService.Interfaces;
using HMT_UserService.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserCRUDValidator>();

builder.Services.AddScoped<ILoggingRepository, LoggingService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCors", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddHttpClient<ILoggingRepository, LoggingService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7193");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My HMT Users Swagger");
    });
}

app.UseHttpsRedirection();
app.UseCors("OpenCors");
app.MapControllers();
app.Run();