using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Domain.Models;

// Modellklass för adressinformation kopplad till en kund
public class AddressModel
{
    public string StreetAddress { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}
