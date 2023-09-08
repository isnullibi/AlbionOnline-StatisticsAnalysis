using Avalonia.Controls;
using Avalonia.Input;
using Serilog;
using StatisticsAnalysisTool.Avalonia.ViewModels;
using System;
using System.Reflection;

namespace StatisticsAnalysisTool.Avalonia.Views;

public partial class ToolLoadingWindow : Window
{
    // This ctor exists only for the previewer in an IDE
    public ToolLoadingWindow()
    {
        InitializeComponent();
    }
    public ToolLoadingWindow(ToolLoadingWindowViewModel toolLoadingWindowViewModel)
    {
        InitializeComponent();
        DataContext = toolLoadingWindowViewModel;
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
}