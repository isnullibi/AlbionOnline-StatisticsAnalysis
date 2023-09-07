using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsAnalysisTool.Avalonia.Common;
public static class Utilities
{
    public static long GetHighestLength(params Array[] arrays)
    {
        long highestLength = 0;

        foreach (var array in arrays)
        {
            if (array.Length > highestLength)
            {
                highestLength = array.Length;
            }
        }

        return highestLength;
    }
    
    public static bool IsWindowOpen<T>(string name = "") where T : Window
    {
        throw new NotImplementedException();
    }

    public static double GetValuePerHourToDouble(double value, double seconds)
    {
        try
        {
            var hours = seconds / 60d / 60d;
            return value / hours;
        }
        catch (OverflowException)
        {
            return double.MaxValue;
        }
    }

    public static double GetValuePerSecondToDouble(double value, DateTime? combatStart, TimeSpan time, double maxValue = -1)
    {
        if (double.IsInfinity(value)) return maxValue > 0 ? maxValue : double.MaxValue;

        if (time.Ticks <= 1 && combatStart != null)
        {
            var startTimeSpan = DateTime.UtcNow - (DateTime) combatStart;
            var calculation = value / startTimeSpan.TotalSeconds;
            return calculation > maxValue ? maxValue : calculation;
        }

        var valuePerSeconds = value / time.TotalSeconds;
        if (maxValue > 0 && valuePerSeconds > maxValue) return maxValue;

        return valuePerSeconds;
    }

    public static bool IsBlockingTimeExpired(DateTime dateTime, int waitingSeconds)
    {
        var currentDateTime = DateTime.UtcNow;
        var difference = currentDateTime.Subtract(dateTime);
        return difference.Seconds >= waitingSeconds;
    }
}
