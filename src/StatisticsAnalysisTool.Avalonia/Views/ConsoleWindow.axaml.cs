using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using StatisticsAnalysisTool.Avalonia.Common;
using System;

namespace StatisticsAnalysisTool.Avalonia.Views;
public partial class ConsoleWindow : Window
{
    public ConsoleWindow()
    {
        InitializeComponent();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();

    private void MinimizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

    private void MaximizedButton_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
            MaximizeButton.IsVisible = true;
            RestoreButton.IsVisible = false;
        }
        else
        {
            WindowState = WindowState.Maximized;
            MaximizeButton.IsVisible = false;
            RestoreButton.IsVisible = true;
        }
    }

    private void Grid_DoubleTapped(object sender, TappedEventArgs e)
    {
        if (WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
        else WindowState = WindowState.Maximized;
    }

    private void Hotbar_Holding(object sender, PointerPressedEventArgs e)
    {
        try
        {
            BeginMoveDrag(e);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void BtnStartConsole_Click(object sender, RoutedEventArgs e)
    {
        ConsoleManager.Start();
    }

    private void BtnStopConsole_Click(object sender, RoutedEventArgs e)
    {
        ConsoleManager.Stop();
    }

    private void BtnResetConsole_Click(object sender, RoutedEventArgs e)
    {
        ConsoleManager.Reset();
    }
}
