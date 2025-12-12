using APIExamen.Contexto;
using APIExamen.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Se agreaga el contexto de la base de datos
builder.Services.AddDbContext<ContextoBaseDatos>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));

//Inyección del servicio de estudiantes
builder.Services.AddScoped<EstudianteService>();

// Configuración de CORS permitiendo todas las solicitudes
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin() // Permite cualquier origen
                   .AllowAnyMethod() // Permite cualquier método (GET, POST, etc.)
                   .AllowAnyHeader(); // Permite cualquier encabezado
        });
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
