using System.Text.RegularExpressions;

namespace Utilities.Helpers;

public class ValitadionHelpers
{
    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }


    public static bool IsNumeric(string input) =>
        double.TryParse(input, out _);


    /// <summary>
    /// Valida si el número de documento tiene el formato correcto (ej: Cédula de ciudadanía, DNI, etc.)
    /// </summary>
    public static bool IsValidDocumentNumber(string documentNumber)
    {
        // Validar si el documento tiene un formato numérico con una longitud específica, por ejemplo, 8 dígitos.
        // Esta expresión regular debe ajustarse al tipo de documento que estés validando.
        var pattern = @"^\d{8,10}$"; // Ejemplo de validación para un número entre 8 y 10 dígitos.
        return Regex.IsMatch(documentNumber, pattern);
    }


    /// <summary>
    /// Valida si el número de teléfono tiene el formato correcto (por ejemplo, con un código de país y una longitud válida).
    /// </summary>
    public static bool IsValidPhoneNumber(string phone)
    {
        // Validar formato de teléfono: opcionalmente puede empezar con un "+", seguido por 9-15 dígitos.
        var pattern = @"^\+?\d{9,15}$"; 
        return Regex.IsMatch(phone, pattern);
    }


    /// <summary>
    /// Normaliza un nombre o apellido para asegurar que esté en el formato correcto (primera letra mayúscula).
    /// </summary>
    public static string NormalizeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return string.Empty;

        // Capitalizar la primera letra y poner el resto en minúsculas.
        return char.ToUpper(name[0]) + name.Substring(1).ToLower();
    }
}
