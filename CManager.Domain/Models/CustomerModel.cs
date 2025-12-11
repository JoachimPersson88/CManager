using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CManager.Domain.Models;

// Modellklass som representerar en kund i systemet
public class CustomerModel
{
    // Unikt Id för kunden, skapas med Guid
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public AddressModel Address { get; set; } = null!;
}
