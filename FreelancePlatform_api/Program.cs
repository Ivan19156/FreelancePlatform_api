using DataAccess.Services;
using FreelancePlatform.Core.Services;
using Microsoft.EntityFrameworkCore;
using DataAccess.Date;


var builder = WebApplication.CreateBuilder(args);

// Налаштування CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Реєстрація необхідних сервісів
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Додаємо реєстрацію DbContext (замість "YourDbContext" використайте свій контекст)
builder.Services.AddDbContext<FreelancePlatformDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Додаємо реєстрацію UserService
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Використання CORS
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FreelancerPlatform API V1");
        options.RoutePrefix = string.Empty;
    });
}

// Middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
