using Microsoft.EntityFrameworkCore;
using BovineBoss_API.Models.DB;
using BovineBoss_API.Services.Contrato;
using BovineBoss_API.Services.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BovineBossContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

//Aca se trabaja la inyeccion de dependencias, de la relacion entre la interfaz y la implementacion
//Para inyectar estos servicios
builder.Services.AddScoped<IAdminService, PersonaService>();

builder.Services.AddScoped<IFincaService, FincaService>();



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
