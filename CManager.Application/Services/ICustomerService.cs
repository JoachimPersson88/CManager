using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Application.Services;

// Interface som beskriver vilka metoder CustomerService måste ha
public interface ICustomerService
{
    // Skapar en ny kund och returnerar true/false om det lyckades
    bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city);

    CustomerModel? GetCustomerByEmail(string email);
    // Hämtar en kund baserat på e-postadress, returnerar null om ingen kund hittas

    // Hämtar alla kunder och anger via hasError om något gick fel
    IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);
}
