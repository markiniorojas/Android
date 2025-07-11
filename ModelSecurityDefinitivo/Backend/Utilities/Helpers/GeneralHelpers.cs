namespace Utilities.Helpers;

public static class GeneralHelpers
{
    public static string GenerateRandomCode(int length)
    {
        var random = new Random();
        return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string NormalizeString(string input) =>
        input?.Trim().ToLowerInvariant();
}
