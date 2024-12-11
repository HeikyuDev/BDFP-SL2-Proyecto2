using Crud.Server.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de la base de datos
builder.Services.AddDbContext<DbcrudSeminarioContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

// Configuraci�n de CORS
builder.Services.AddCors(opciones => {
    opciones.AddPolicy("nuevaPolitica", app =>
    {
        app.AllowAnyOrigin()  // Permite cualquier origen (ideal solo para desarrollo)
           .AllowAnyHeader()   // Permite cualquier encabezado
           .AllowAnyMethod();  // Permite cualquier m�todo HTTP
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplica la pol�tica de CORS antes de la autorizaci�n
app.UseCors("nuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
