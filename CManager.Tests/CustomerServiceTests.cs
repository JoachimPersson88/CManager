using CManager.Application.Services;
using CManager.Domain.Models;
using CManager.Infrastructure.Repos;
using Moq;


public class CustomerServiceTests
{

    // Test 1
    [Fact] // xUnit attribut för att markera en testmetod
    public void FindCustomer_Test()
    {
        // Förbereder testdata och mock
        var repoMock = new Mock<ICustomerRepo>();

        // Skapa en lista med kunder för testet
        var customers = new List<CustomerModel>
        {
            new CustomerModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Zlatan",
                LastName = "Persson",
                Email = "zlatan@gmail.com",
                PhoneNumber = "0754254597",
                Address = new AddressModel
                {
                    StreetAddress = "Zlatans gata",
                    PostalCode = "12345",
                    City = "Malmö"
                }
            }
        };

        // Ställer in mocken att returnera testkunderna
        repoMock.Setup(r => r.GetAllCustomers()).Returns(customers);

        // Skapar instansen av tjänsten med den mockade repot
        var service = new CustomerService(repoMock.Object);

        // Kör metoden
        var result = service.GetCustomerByEmail("zlatan@gmail.com");

        // Assert (verifiera resultat)
        Assert.NotNull(result);
        Assert.Equal("zlatan@gmail.com", result!.Email);
        Assert.Equal("Zlatan", result.FirstName);
    }

    // Test 2
    [Fact]
    public void FailedToFindCustomer_Test()
    {
        var repoMock = new Mock<ICustomerRepo>();

        var customers = new List<CustomerModel>
    {
        new CustomerModel
        {
            Id = Guid.NewGuid(),
            FirstName = "Henrik",
            LastName = "Larsson",
            Email = "henke@gmail.com",
            PhoneNumber = "0701111111",
            Address = new AddressModel
            {
                StreetAddress = "Hemma hos henke",
                PostalCode = "54321",
                City = "Helsingborg"
            }
        }
    };

        repoMock.Setup(r => r.GetAllCustomers()).Returns(customers);

        var service = new CustomerService(repoMock.Object);

        var result = service.GetCustomerByEmail("kanejhittas@gmail.com");

        Assert.Null(result);
    }
}


