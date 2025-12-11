using System.Text.RegularExpressions;

namespace CManager.Presentation.ConsoleApp.Helpers;

// Typer av validering som kan användas vid inmatning
public enum ValidationType
{
    Required,   // Fältet måste vara ifyllt
    Email,      // Fältet måste vara en giltig e-postadress
}

// Hjälpklass för att fråga användaren om input och validera den
public static class InputHelper
{
    // Frågar användaren om ett värde och validerar det enligt vald ValidationType
    public static string ValidateInput(string fieldName, ValidationType validationType)
    {
        while (true)
        {
            Console.Write($"{fieldName}: ");
            var input = Console.ReadLine()!;

            // Kontrollerar om användaren lämnat fältet tomt
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"{fieldName} is required. Press any key to try again....");
                Console.ReadKey();
                continue; // Börja om loopen
            }

            // Validerar inmatningen utifrån vald typ (Required/Email)
            var (isValid, errorMessage) = ValidateType(input, validationType);

            if (isValid)
                return input; // Returnerar värdet om det är giltigt

            // Om ogiltigt: visa felmeddelande och låt användaren försöka igen
            Console.WriteLine($"{errorMessage}. Press any key to continue:");
            Console.ReadKey();
        }
    }

    // Utför den konkreta valideringen beroende på ValidationType
    private static (bool isValid, string errorMessage) ValidateType(string input, ValidationType type)
    {
        switch (type)
        {
            case ValidationType.Required:
                // Required är alltid ok här eftersom vi redan har kollat tom sträng
                return (true, "");

            case ValidationType.Email:
                if (IsValidEmail(input))
                {
                    return (true, "");
                }
                else
                {
                    // Felmeddelande om e-postformatet är ogiltigt
                    return (false, "Inavlid email. Use name@example.com ");
                }

            default:
                // Default: ingen extra validering
                return (true, "");
        }
    }

    // Kontrollerar om en sträng är en giltig e-post med hjälp av regex
    private static bool IsValidEmail(string input)
    {
        var pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        return Regex.IsMatch(input, pattern);
    }
}
