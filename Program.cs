using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Thebook.AutoMappers;
using Thebook.DTOs;
using Thebook.Middlewares;
using Thebook.Models;
using Thebook.Repository;
using Thebook.Services;
using Thebook.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conexion a DB
builder.Services.AddDbContext<TheBookContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MiConexion")));

//Inyeccion de dependencias Repositorys y Services
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<ILibroRepository, LibroRepository>();
builder.Services.AddScoped<ILibroService, LibroService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPrestamoRepository, PrestamoRepository>();
builder.Services.AddScoped<IPrestamoService, PrestamoService>();
builder.Services.AddScoped<IDevolucionRepository, DevolucionRepository>();
builder.Services.AddScoped<IDevolucionService, DevolucionService>();
builder.Services.AddScoped<IReportesService, ReportesService>();

//Mappers
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

//Validaciones 
builder.Services.AddScoped<IValidator<EmpleadoInsertDto>, EmpleadoInsertValidator>();
builder.Services.AddScoped<IValidator<EmpleadoUpdateDto>,  EmpleadoUpdateValidator>();
builder.Services.AddScoped<IValidator<LibroInsertDto>, LibroInsertValidator>();
builder.Services.AddScoped<IValidator<LibroUpdateDto>, LibroUpdateValidator>();
builder.Services.AddScoped<IValidator<UsuarioInsertDto>, UsuarioInsertValidator>();
builder.Services.AddScoped<IValidator<UsuarioUpdateDto>, UsuarioUpdateValidator>();
builder.Services.AddScoped<IValidator<PrestamoInsertDto>, PrestamoInsertValidator>();

//jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware para Excepsiones
app.UseMiddleware<ExceptionMiddleware>();

//jwt
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
