using api_web_services_fuel_manager.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;                    // depend�ncias para a utiliza��o da ForeignKey. O ve�culo aponta para o consumo
using System.Text.Json.Serialization; // e o consumo aponta para o veiculo, pode haver um ciclo infinito entre essas requisi��es
                                      // causando erro nas requisi��es http. � necess�rio evitar esses ciclos quando a resposta vem
                                      // em J�S�n

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
// configura��o do cliclo do JSON
     .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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