using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Data.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ef core
builder.Services.AddDbContext<SamuraiContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("SamuraiConnection"))
.EnableSensitiveDataLogging());

//injection
builder.Services.AddTransient<ISamurai, SamuraiRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
