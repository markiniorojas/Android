using System.Text.RegularExpressions;

namespace Utilities.Helpers;

public class PasswordHelpers
{
    private static Random random = new Random();

    public static string GenerateRandomPassword(int length = 12)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_.-";
        const string specialChars = "_.-";
        const string digits = "0123456789";

        while (true)
        {
            var passwordChars = new char[length];

            // Asegurar que al menos un dígito y un carácter especial estén incluidos:
            passwordChars[0] = digits[random.Next(digits.Length)];
            passwordChars[1] = specialChars[random.Next(specialChars.Length)];

            // Completar el resto con caracteres aleatorios válidos
            for (int i = 2; i < length; i++)
            {
                passwordChars[i] = chars[random.Next(chars.Length)];
            }

            // Mezclar los caracteres para no tener los especiales al inicio siempre
            passwordChars = passwordChars.OrderBy(x => random.Next()).ToArray();

            var password = new string(passwordChars);

            if (IsValidPassword(password))
                return password;
        }
    }


    /// <summary>
    /// Valida si una contraseña cumple con los siguientes requisitos:
    /// - Mínimo 8 caracteres
    /// - Al menos un número
    /// - Al menos uno de los siguientes caracteres especiales: _ . -
    /// </summary>
    public static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;

        var pattern = @"^(?=.*\d)(?=.*[_\.\-])[A-Za-z\d_\.\-]{8,}$";

        return Regex.IsMatch(password, pattern);
    }
}
