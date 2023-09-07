using System;
using System.Collections.Generic;
using System.Text.Json;
using StatisticsAnalysisTool.Avalonia.Enumerations;

namespace StatisticsAnalysisTool.Avalonia.Models;
public class ConsoleFragment
{
    public ConsoleFragment(string? eventName, string? text, ConsoleColorType consoleColorType)
    {
        Timestamp = DateTime.Now;
        EventName = eventName ?? string.Empty;
        Text = text ?? string.Empty;
        ConsoleColorType = consoleColorType;
    }

    public ConsoleFragment(string? eventName, Dictionary<byte, object> parameters, ConsoleColorType consoleColorType)
    {
        Timestamp = DateTime.UtcNow;
        EventName = eventName ?? string.Empty;
        Text = JsonSerializer.Serialize(parameters);
        ConsoleColorType = consoleColorType;
    }

    public DateTime Timestamp { get; set; }
    public string EventName { get; set; }
    public string Text { get; set; }
    public ConsoleColorType ConsoleColorType { get; set; }
    public string Output => $"[{Timestamp}] {EventName}: {Text}";
}
