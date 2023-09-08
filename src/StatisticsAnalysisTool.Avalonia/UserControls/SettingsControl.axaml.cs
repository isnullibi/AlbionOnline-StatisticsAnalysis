using Avalonia.Controls;
using Avalonia.Interactivity;
using Serilog;
using StatisticsAnalysisTool.Avalonia.Common.Shortcut;
using StatisticsAnalysisTool.Avalonia.ViewModels;
using System;
using System.Diagnostics;
using System.Reflection;

namespace StatisticsAnalysisTool.Avalonia.UserControls;

public partial class SettingsControl : UserControl
{
    private readonly SettingsWindowViewModel _settingsWindowViewModel;
    public SettingsControl()
    {
        InitializeComponent();
        _settingsWindowViewModel = new SettingsWindowViewModel();
        DataContext = _settingsWindowViewModel;
    }

    private void OpenDebugConsole_Click(object sender, RoutedEventArgs e)
    {
        return;
    }

    private void CreateDesktopShortcut_Click(object sender, RoutedEventArgs e)
    {
        ShortcutController.CreateShortcut();
    }

    private void OpenToolDirectory_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _ = Process.Start(new ProcessStartInfo { FileName = SettingsWindowViewModel.ToolDirectory, UseShellExecute = true });
        }
        catch (Exception ex)
        {
            //_ = MessageBox.Show(ex.Message, LanguageController.Translation("ERROR"));
            //ConsoleManager.WriteLineForError(MethodBase.GetCurrentMethod()?.DeclaringType, ex);
            Log.Error(ex, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
        }
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        _settingsWindowViewModel.SaveSettings();
    }

    private async void CheckForUpdate_Click(object sender, RoutedEventArgs e)
    {
        return;
    }

    private void ResetPacketFilter_Click(object sender, RoutedEventArgs e)
    {
        _settingsWindowViewModel.ResetPacketFilter();
    }

    private void ResetPlayerSelectionWithSameNameInDb_Click(object sender, RoutedEventArgs e)
    {
        return;
    }

    private async void BackupNow_Click(object sender, RoutedEventArgs e)
    {
        return;
    }
}