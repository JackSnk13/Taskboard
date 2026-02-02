using TaskBoardAPI.Data;
using TaskBoardAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔗 Conexión a base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🛡️ Servicio de autenticación
builder.Services.AddScoped<AuthService>();

// 📦 Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskBoard API", Version = "v1" });
});

// ✅ Configuración de CORS para permitir Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // frontend Angular
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

// 🧰 Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Activar CORS antes de MapControllers
app.UseCors("AllowAngular");

app.UseAuthorization();

// ✅ Mapear controladores
app.MapControllers();

// 🔐 Generador de hash de prueba (puedes quitar esto en producción)
string plainPassword = "admin123";
string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);
Console.WriteLine("Hash generado:");
Console.WriteLine(hashedPassword);

app.Run();
