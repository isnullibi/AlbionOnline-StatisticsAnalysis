using Serilog;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using System;
using System.Reflection;

namespace StatisticsAnalysisTool.Avalonia.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private static bool _isWindowMaximized;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Hotbar_Holding(object sender, PointerPressedEventArgs e)
    {
        try
        {
            BeginMoveDrag(e);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
        }
    }

    private void CloseButton_Click(object? sender, RoutedEventArgs e)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp)
        {
            desktopApp.Shutdown();
        }
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        if (_isWindowMaximized)
        {
            RestoreWindow();
        }
        else
        {
            MaximizeWindow();
        }
    }

    private void Grid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (_isWindowMaximized)
        {
            RestoreWindow();
        }
        else
        {
            MaximizeWindow();
        }
    }

    private void MaximizeWindow()
    {
        WindowState = WindowState.Maximized;
        _isWindowMaximized = true;
        MaximizeButton.IsVisible = false;
        RestoreButton.IsVisible = true;
    }

    private void RestoreWindow()
    {
        WindowState = WindowState.Normal;
        _isWindowMaximized = false;
        MaximizeButton.IsVisible = true;
        RestoreButton.IsVisible = false;
    }

    private void MainWindow_OnClosing(object sender, WindowClosingEventArgs e)
    {
        return;
    }

    #region Error bar test
    private void TestButton(object sender, RoutedEventArgs e)
    {
        ErrorBar.ErrorText = "Text, Man";
    }

    #endregion
}