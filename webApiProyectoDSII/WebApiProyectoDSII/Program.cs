using WebApiProyectoDSII.Models;
using Microsoft.EntityFrameworkCore;
using WebApiProyectoDSII.Repositories;
using WebApiProyectoDSII.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TransporteFloresDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});

// Registro de repositorios y servicios para inyección
builder.Services.AddScoped<EnviosRepositories>();
builder.Services.AddScoped<ProyecctionService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        app =>
        {
            app.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
