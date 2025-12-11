using CManager.Application.Services;
using CManager.Infrastructure.Repos;
using CManager.Presentation.ConsoleApp.Controllers;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        // Skapar en ServiceCollection som används för Dependency Injection
        var services = new ServiceCollection()
            .AddScoped<ICustomerService, CustomerService>() // Registrerar CustomerService som implementation av ICustomerService
            .AddScoped<ICustomerRepo, CustomerRepo>()       // Registrerar CustomerRepo som implementation av ICustomerRepo
            .AddScoped<MenuController>()                    // Registrerar MenuController så den kan få sina beroenden injicerade
            .BuildServiceProvider();                        // Bygger en ServiceProvider som kan skapa objekten åt oss

        // Hämtar en instans av MenuController från DI-containern
        var controller = services.GetRequiredService<MenuController>();
        // Startar meny-loopen
        controller.ShowMenu();
    }
}
