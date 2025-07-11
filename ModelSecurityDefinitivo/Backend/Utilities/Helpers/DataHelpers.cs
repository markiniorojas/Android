namespace Utilities.Helpers;

public class DataHelpers
{
    public static string FormatDate(DateTime date) => date.ToString("yyyy-MM-dd");

    public static bool IsWeekend(DateTime date) =>
        date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    
}
