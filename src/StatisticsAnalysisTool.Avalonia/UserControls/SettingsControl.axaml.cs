using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using StatisticsAnalysisTool.Avalonia.Common;
using StatisticsAnalysisTool.Avalonia.Common.Shortcut;
using Avalonia.Markup.Xaml;
using StatisticsAnalysisTool.Avalonia.ViewModels;

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

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        return;
    }

    private async void CheckForUpdate_Click(object sender, RoutedEventArgs e)
    {
        return;
    }

    private void ResetPacketFilter_Click(object sender, RoutedEventArgs e)
    {
        return;
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