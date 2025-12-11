using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Infrastructure.Repos;

// Interface som beskriver vilka metoder ett kund-repository ska ha
public interface ICustomerRepo
{
    // Hämtar alla kunder från lagringen (filen)
    List<CustomerModel> GetAllCustomers();

    // Sparar en lista med kunder till lagringen och returnerar true/false
    bool SaveCustomers(List<CustomerModel> customers);
}
