using Microsoft.EntityFrameworkCore;
using pruebaTecnica.config;
using pruebaTecnica.repository;
using pruebaTecnica.services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IEmployeesRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeesService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();
