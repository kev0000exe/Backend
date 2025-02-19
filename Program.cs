using Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductoAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar la conexi�n a la base de datos

Console.WriteLine("Connection String: " + builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
