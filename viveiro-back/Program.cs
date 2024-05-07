using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using viveiro_back.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<viveiro_backContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("viveiro_backContext") ?? throw new InvalidOperationException("Connection string 'viveiro_backContext' not found.")));

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin(); // Permite cualquier origen
            builder.AllowAnyMethod(); // Permite cualquier método HTTP
            builder.AllowAnyHeader(); // Permite cualquier header
        });
});

string port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});

builder.Services.AddControllers();
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
