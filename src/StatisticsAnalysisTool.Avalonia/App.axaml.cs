using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Serilog;
using StatisticsAnalysisTool.Avalonia.Common;
using StatisticsAnalysisTool.Avalonia.Common.UserSettings;
using StatisticsAnalysisTool.Avalonia.ViewModels;
using StatisticsAnalysisTool.Avalonia.Views;
using System;
using System.IO;
using System.Reflection;

namespace StatisticsAnalysisTool.Avalonia;
public partial class App : Application
{
#pragma warning disable CS8618
    private MainWindowViewModel _mainWindowViewModel;
#pragma warning restore CS8618

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.Startup += OnStartup;
            desktop.Exit += OnExit;
        }

        base.OnFrameworkInitializationCompleted();

    }

    private async void OnStartup(object? sender, ControlledApplicationLifetimeStartupEventArgs args)
    {
        InitLogger();
        
        if (sender is not IClassicDesktopStyleApplicationLifetime desktop)
        {
            Log.Fatal("Unsupported platform");
            throw new Exception();
        }

        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

        Log.Information($"Tool started with v{Assembly.GetExecutingAssembly().GetName().Version}");

        SettingsController.LoadSettings();

        //Settings loading
        //Lang init

        //Update & backup

        RegisterServicesEarly();

        desktop.MainWindow = new MainWindow
        {
            DataContext = _mainWindowViewModel,
        };

        await _mainWindowViewModel.InitMainWindowDataAsync();
        desktop.MainWindow.Show();
    }

    private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        CriticalData.Save();
    } 

    private void RegisterServicesEarly()
    {
        _mainWindowViewModel = new MainWindowViewModel();
        ServiceLocator.Register<MainWindowViewModel>(_mainWindowViewModel);
    }
    private static void InitLogger()
    {
        const string logFolderName = "logs";
        DirectoryController.CreateDirectoryWhenNotExists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFolderName));

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(
                Path.Combine(logFolderName, "sat-.logs"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
            .MinimumLevel.Verbose()
            .WriteTo.Debug(
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
            .CreateLogger();
    }

    private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        try
        {
            Log.Fatal(e.ExceptionObject as Exception, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
        }
    }
}