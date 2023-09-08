using Serilog;
using Avalonia.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

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

    public static void AnotherAppToStart(string? path)
    {
        // notify manager
        if (string.IsNullOrEmpty(path)) return;

        try
        {
            if (!File.Exists(path)) return;

            Process.Start(path);
        }
        catch (Exception e)
        {
            Log.Error(e, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
        }
    }
}
