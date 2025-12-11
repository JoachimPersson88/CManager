using CManager.Domain.Models;          // Importerar domänmodellerna (CustomerModel, AddressModel)
using CManager.Infrastructure.Repos;   // Importerar repository-gränssnittet för kunder
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Application.Services;

// Serviceklass som implementerar logik för att hantera kunder
public class CustomerService : ICustomerService
{
    // Fält för att komma åt repositoryt via interface (Dependency Injection)
    private readonly ICustomerRepo _customerRepo;

    // Konstruktorn tar emot ett ICustomerRepo och sätter det i fältet
    public CustomerService(ICustomerRepo customerRepo)
    {
        _customerRepo = customerRepo;
    }

    // Skapar en ny kund och sparar den i fil via repositoryt
    public bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city)
    {
        // Bygger upp ett nytt CustomerModel-objekt med Guid som unikt Id
        CustomerModel customerModel = new()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = new AddressModel
            {
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                City = city
            }
        };

        try
        {
            // Hämtar alla kunder från filen
            var customers = _customerRepo.GetAllCustomers();
            // Lägger till den nya kunden i listan
            customers.Add(customerModel);
            // Sparar tillbaka listan till fil
            var result = _customerRepo.SaveCustomers(customers);
            // Returnerar true/false beroende på om sparandet lyckades
            return result;
        }
        catch (Exception)
        {
            // Vid fel returneras false
            return false;
        }
    }

    // Hämtar alla kunder och indikerar om något fel uppstod via out-parametern
    public IEnumerable<CustomerModel> GetAllCustomers(out bool hasError)
    {
        hasError = false; // Anta inget fel från början

        try
        {
            // Hämtar alla kunder från repositoryt
            var customers = _customerRepo.GetAllCustomers();
            return customers;
        }
        catch (Exception)
        {
            // Om ett fel inträffar sätts hasError till true
            // här kommer throw från CustomerRepo.GetAllCustomers hamna
            hasError = true;
            // Returnerar en tom lista om något går fel
            return [];
        }
    }
}
