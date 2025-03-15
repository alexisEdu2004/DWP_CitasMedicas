using DWP_CitasMedicas.Models;
using DWP_CitasMedicas.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuraci�n de la base de datos
        builder.Services.AddDbContext<DwpContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("connectionDB"))
        );

       

        // Add services to the container.
        builder.Services.AddControllers();

        // Configuraci�n de Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configuraci�n de CORS
        var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins(origenesPermitidos) // Permite solicitudes desde estos or�genes
                      .AllowAnyHeader() // Permite cualquier encabezado
                      .AllowAnyMethod(); // P
            });
        });

        // Servicio de email (si es necesario)
        builder.Services.AddTransient<IServicioEmail, ServicioEmail>();

        var app = builder.Build();

        // Configuraci�n del pipeline de HTTP
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); // Habilitar p�ginas de excepci�n detalladas en desarrollo
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error"); // Manejo de errores en producci�n
            app.UseHsts(); // Habilitar HSTS en producci�n
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowFrontend");

        // Autenticaci�n 
        // app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}