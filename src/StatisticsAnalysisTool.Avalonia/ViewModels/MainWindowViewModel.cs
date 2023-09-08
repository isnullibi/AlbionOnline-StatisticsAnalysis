using CommunityToolkit.Mvvm.ComponentModel;
using StatisticsAnalysisTool.Avalonia.Models.NetworkModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace StatisticsAnalysisTool.Avalonia.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    #region Bindings

    [ObservableProperty] private bool _isDebugMode;
    [ObservableProperty] private bool _isOsUnsupported;
    [ObservableProperty] private bool _isCloseButtonActive;
    [ObservableProperty] private bool _isDashboardTabVisible = true;
    [ObservableProperty] private bool _isItemSearchTabVisible = true;
    [ObservableProperty] private bool _isLoggingTabVisible = true;
    [ObservableProperty] private bool _isDungeonsTabVisible = true;
    [ObservableProperty] private bool _isDamageMeterTabVisible = true;
    [ObservableProperty] private bool _isTradeMonitoringTabVisible = true;
    [ObservableProperty] private bool _isGatheringTabVisible = true;
    [ObservableProperty] private bool _isPartyBuilderTabVisible = true;
    [ObservableProperty] private bool _isStorageHistoryTabVisible = true;
    [ObservableProperty] private bool _isMapHistoryTabVisible = true;
    [ObservableProperty] private bool _isPlayerInformationTabVisible = true;
    [ObservableProperty] private bool _isToolTaskFrontViewVisible = false;
    [ObservableProperty] private bool _isStatsDropDownVisible = false;
    [ObservableProperty] private int _partyMemberNumber = 0;
    [ObservableProperty] private double _toolTaskProgressBarValue = 0;
    [ObservableProperty] private string _errorText = string.Empty;
    [ObservableProperty] private string _toolTaskCurrentTaskName = string.Empty;

    [ObservableProperty] private ObservableCollection<PartyMemberCircle> _partyMemberCircles = new();


    #endregion

    public MainWindowViewModel()
    {
        SetUiElements();
    }

    public void SetUiElements()
    {
        // Unsupported OS
        IsOsUnsupported = Environment.OSVersion.Version.Major < 10;
    }

    public async Task InitMainWindowDataAsync()
    {
#if DEBUG
        IsDebugMode = true;
#endif
        CloseButtonActivationDelayAsync();
    }

    private async void CloseButtonActivationDelayAsync()
    {
        await Task.Delay(2000);
        IsCloseButtonActive = true;
    }

    #region Error bar

    public void SetErrorBar(string errorMessage)
    {
        ErrorText = errorMessage;
    }

    #endregion
}
