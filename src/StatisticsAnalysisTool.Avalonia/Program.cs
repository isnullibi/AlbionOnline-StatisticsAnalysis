using Serilog;
using Avalonia;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using System;
using System.IO;
using System.Reflection;

namespace StatisticsAnalysisTool.Avalonia;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        InitLogger();
        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch(Exception ex)
        {
            Log.Fatal(ex, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
        }
        
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        IconProvider.Current
            .Register<FontAwesomeIconProvider>();

        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }

    private static void InitLogger()
    {
        //const string logFolerName = "logs";
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sat-.logs"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
            .MinimumLevel.Verbose()
            .CreateLogger();
    }
}
