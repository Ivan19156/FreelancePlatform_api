using DataAccess.Services;
using FreelancePlatform.Core.Services;
using Microsoft.EntityFrameworkCore;
using DataAccess.Date;


var builder = WebApplication.CreateBuilder(args);

// ������������ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ��������� ���������� ������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ������ ��������� DbContext (������ "YourDbContext" ������������ ��� ��������)
builder.Services.AddDbContext<FreelancePlatformDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ������ ��������� UserService
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// ������������ CORS
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
