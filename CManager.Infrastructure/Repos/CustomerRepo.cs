using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CManager.Infrastructure.Repos;

// Repositoryklass som ansvarar för att läsa/skriva kunder till JSON-fil
public class CustomerRepo : ICustomerRepo
{
    // Full sökväg till filen med kunddata
    private readonly string _filePath;
    // Sökväg till katalogen där filen ska ligga
    private readonly string _directoryPath;
    // Inställningar för JSON-serialisering
    private readonly JsonSerializerOptions _jsonOptions;

    // Konstruktorn sätter standardkatalog och filnamn om inget annat anges
    public CustomerRepo(string directoryPath = "Data", string fileName = "list.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,            // Gör JSON lättare att läsa (indenterad)
            PropertyNameCaseInsensitive = true, // Ignorerar skillnad på stora/små bokstäver i property-namn
        };
    }

    // Hämtar alla kunder från JSON-filen
    public List<CustomerModel> GetAllCustomers()
    {
        // Om filen inte finns returneras en tom lista
        if (!File.Exists(_filePath))
        {
            return [];
        }

        try
        {
            // Läser in hela filens innehåll som text
            var json = File.ReadAllText(_filePath);
            // Försöker deserialisera texten till en lista av CustomerModel
            var customers = JsonSerializer.Deserialize<List<CustomerModel>>(json, _jsonOptions);
            // Om deserialisering ger null returneras en tom lista i stället
            return customers ?? [];
        }
        catch (Exception ex)
        {
            // Skriver felmeddelande i konsolen och kastar vidare felet
            Console.WriteLine($"Error loading customers: {ex.Message}");
            throw;
        }
    }

    // Sparar en lista med kunder till JSON-filen
    public bool SaveCustomers(List<CustomerModel> customers)
    {
        // Om listan är null går det inte att spara
        if (customers == null)
            return false;

        try
        {
            // Serialiserar listan till JSON-sträng
            var json = JsonSerializer.Serialize(customers, _jsonOptions);

            // Skapar katalogen om den inte redan finns
            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            // Skriver JSON-strängen till filen
            File.WriteAllText(_filePath, json);
            return true; // Spara lyckades
        }
        catch (Exception ex)
        {
            // Skriver felmeddelande och returnerar false om något går fel
            Console.WriteLine($"Error saving customers: {ex.Message}");
            return false;
        }
    }
}
