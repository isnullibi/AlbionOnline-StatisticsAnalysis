using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading.Tasks;

namespace StatisticsAnalysisTool.Avalonia.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    #region Bindings

    [ObservableProperty] private string _errorText = string.Empty;
    [ObservableProperty] private bool _isDebugMode;
    [ObservableProperty] private bool _isOsUnsupported;
    [ObservableProperty] private bool _isCloseButtonActive;
    [ObservableProperty] private int _partyMemberNumber = 0;
    [ObservableProperty] private double _toolTaskProgressBarValue = 0;
    [ObservableProperty] private bool _dashboardTabVisibility = true;
    [ObservableProperty] private bool _itemSearchTabVisibility = true;
    [ObservableProperty] private bool _loggingTabVisibility = true;
    [ObservableProperty] private bool _dungeonsTabVisibility = true;
    [ObservableProperty] private bool _damageMeterTabVisibility = true;
    [ObservableProperty] private bool _tradeMonitoringTabVisibility = true;
    [ObservableProperty] private bool _gatheringTabVisibility = true;
    [ObservableProperty] private bool _partyBuilderTabVisibility = true;
    [ObservableProperty] private bool _storageHistoryTabVisibility = true;
    [ObservableProperty] private bool _mapHistoryTabVisibility = true;
    [ObservableProperty] private bool _playerInformationTabVisibility = true;
    [ObservableProperty] private bool _toolTaskFrontViewVisibility = false;
    [ObservableProperty] private bool _statsDropDownVisibility = false;


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
