using APIDOCKER.Database;
using APIDOCKER.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Nuestra cadena de conexión.
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                            builder.Configuration.GetConnectionString("DefaultConnection");


//Inyeccion DbContext con nuestra cadena de conexión.
builder.Services.AddDbContext<ApplicationDbContext>(options => 
                                                    options.UseSqlServer(connectionString));

// Registrar los servicios
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Registrar el middleware de manejo global de excepciones
app.UseMiddleware<APIDOCKER.Middleware.GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","Ejemplo de API"));

app.Run();
