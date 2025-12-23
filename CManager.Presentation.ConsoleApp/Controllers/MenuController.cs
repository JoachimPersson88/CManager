using CManager.Application.Services;
using CManager.Presentation.ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CManager.Presentation.ConsoleApp.Controllers;

// Controller som hanterar menyflödet i konsolapplikationen
public class MenuController
{
    // Fält för att komma åt kundservicen
    private readonly ICustomerService _customerService;

    // Konstruktorn tar emot en ICustomerService via Dependency Injection
    public MenuController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // Visar huvudmenyn och loopar tills användaren väljer att avsluta
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Customer Manager");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. View All Customers");
            Console.WriteLine("3. View Specific Customer");
            Console.WriteLine("4. Remove Customer");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");

            var option = Console.ReadLine();

            // Hanterar användarens val
            switch (option)
            {
                case "1":
                    CreateCustomer();      // Menyval för att skapa kund
                    break;

                case "2":
                    ViewAllCustomers();   // Menyval för att visa alla kunder
                    break;

                case "3":
                    ViewSpecificCustomer();
                    break;

                case "4":
                    RemoveCustomer();  // Menyval för att ta bort kund
                    break;

                case "0":
                    return;               // Avslutar metoden och därmed programmet

                default:
                    // Felaktigt val → visa meddelande
                    OutputDialog("Invalid option! Press any key to continue...");
                    break;
            }
        }
    }

    // Menyflöde för att skapa en ny kund
    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Create Customer");

        // Läser in och validerar användarens input för varje fält
        var firstName = InputHelper.ValidateInput("First name", ValidationType.Required);
        var lastName = InputHelper.ValidateInput("Last name", ValidationType.Required);
        var email = InputHelper.ValidateInput("Email", ValidationType.Email);
        var phoneNumber = InputHelper.ValidateInput("PhoneNumber", ValidationType.Required);
        var streetAddress = InputHelper.ValidateInput("Address", ValidationType.Required);
        var postalCode = InputHelper.ValidateInput("PostalCode", ValidationType.Required);
        var city = InputHelper.ValidateInput("City", ValidationType.Required);

        // Anropar service-lagret för att skapa kund
        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);

        // Visar resultat för användaren
        if (result)
        {
            Console.WriteLine("Customer created");
            Console.WriteLine($"Name: {firstName} {lastName}");
        }
        else
        {
            Console.WriteLine("Something went wrong. Please try again");
        }

        OutputDialog("Press any key to continue...");
    }

    // Menyflöde för att visa alla kunder
    private void ViewAllCustomers()
    {
        // Rensa konsolen och visa rubrik
        Console.Clear();
        Console.WriteLine("All Customers");

        // Hämtar alla kunder via service-lagret
        var customers = _customerService.GetAllCustomers(out bool hasError);

        // Om något gick fel vid hämtningen visas ett felmeddelande
        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later");
        }

        // Om listan är tom, meddela att inga kunder finns
        if (!customers.Any())
        {
            Console.WriteLine("No customers found");
        }
        else
        {
            // Loopa igenom alla kunder och skriv ut deras info
            foreach (var customer in customers)
            {
                Console.Clear();
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Phone: {customer.PhoneNumber}");
                Console.WriteLine($"Address: {customer.Address.StreetAddress} {customer.Address.PostalCode} {customer.Address.City}");
                Console.WriteLine($"ID: {customer.Id}");
                Console.WriteLine();
            }
        }

        OutputDialog("Press any key to continue...");
    }

    // Menyflöde för att visa en specifik kund baserat på e-post
    private void ViewSpecificCustomer()
    {
        // Rensa konsolen och visa rubrik
        Console.Clear();
        Console.WriteLine("View Specific Customer");

        // Läser in och validerar e-post från användaren
        var email = InputHelper.ValidateInput("Email", ValidationType.Email);

        // Hämtar kunden via service-lagret
        var customer = _customerService.GetCustomerByEmail(email);

        // Visar kundens information eller ett felmeddelande om kunden inte hittades
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
        }
        // Om kunden hittades, visa dess information
        else
        {
            Console.Clear();
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone: {customer.PhoneNumber}");
            Console.WriteLine($"Address: {customer.Address.StreetAddress}");
            Console.WriteLine($"Postal Code: {customer.Address.PostalCode}");
            Console.WriteLine($"City: {customer.Address.City}");
            Console.WriteLine($"ID: {customer.Id}");
        }
        // Vänta på användarens knapptryck innan återgång till menyn
        OutputDialog("Press any key to continue...");
    }

    // Menyflöde för att ta bort en kund baserat på e-post
    private void RemoveCustomer()
    {
        Console.Clear();
        Console.WriteLine("Remove Customer");
        // Läser in och validerar e-post från användaren
        var email = InputHelper.ValidateInput("Email", ValidationType.Email);
        // Anropar service-lagret för att ta bort kunden
        var result = _customerService.RemoveCustomerByEmail(email);

        if (result)
        {
            Console.WriteLine("Customer was removed successfully.");
        }
        else
        {
            Console.WriteLine("Customer not found or something went wrong.");
        }
        OutputDialog("Press any key to continue...");
    }

    // Hjälpmetod för att visa ett meddelande och vänta på knapptryck
    private void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }
}
