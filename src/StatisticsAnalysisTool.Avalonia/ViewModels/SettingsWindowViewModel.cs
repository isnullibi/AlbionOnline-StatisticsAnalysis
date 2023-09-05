using CommunityToolkit.Mvvm.ComponentModel;
using StatisticsAnalysisTool.Avalonia.Models;
using StatisticsAnalysisTool.Avalonia.Enumerations;
using StatisticsAnalysisTool.Avalonia.Common;
using StatisticsAnalysisTool.Avalonia.Common.UserSettings;
using System;
using System.IO;
//using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace StatisticsAnalysisTool.Avalonia.ViewModels;
public partial class SettingsWindowViewModel : ObservableObject
{
    #region Bindings

    [ObservableProperty] private string _packetFilter = string.Empty;
    [ObservableProperty] private string _anotherAppToStartPath = string.Empty;
    [ObservableProperty] private string _albionDataProjectBaseUrlWest = string.Empty;
    [ObservableProperty] private string _albionDataProjectBaseUrlEast = string.Empty;
    [ObservableProperty] private short _playerSelectionWithSameNameInDb = 0;
    [ObservableProperty] private bool _isOpenItemWindowInNewWindowChecked;
    [ObservableProperty] private bool _isLootLoggerSaveReminderActive;
    [ObservableProperty] private bool _isSuggestPreReleaseUpdatesActive;
    [ObservableProperty] private bool _isBackupNowButtonEnabled = true;
    [ObservableProperty] private bool _showInfoWindowOnStartChecked;
    [ObservableProperty] private Bitmap? _anotherAppToStartExeIcon;
    [ObservableProperty] private SettingDataInformation _backupIntervalByDaysSelection;
    [ObservableProperty] private SettingDataInformation _maximumNumberOfBackupsSelection;
    [ObservableProperty] private SettingDataInformation _serverSelection;
    [ObservableProperty] private ObservableCollection<SettingDataInformation> _backupIntervalByDays = new();
    [ObservableProperty] private ObservableCollection<SettingDataInformation> _maximumNumberOfBackups = new();
    [ObservableProperty] private ObservableCollection<SettingDataInformation> _server = new();
    [ObservableProperty] private ObservableCollection<TabVisibilityFilter> _tabVisibilities = new();

    public static string ToolDirectory => AppDomain.CurrentDomain.BaseDirectory;

    #endregion

    public SettingsWindowViewModel()
    {
        InitializeSetting();
    }

    public void InitializeSetting()
    {
        InitNaviTabVisibilities();
        InitServer();

        // Backup interval by days
        InitDropDownDownByDays(BackupIntervalByDays);
        BackupIntervalByDaysSelection = BackupIntervalByDays.FirstOrDefault(x => x.Value == SettingsController.CurrentSettings.BackupIntervalByDays);

        // Maximum number of backups
        InitMaxAmountOfBackups(MaximumNumberOfBackups);
        MaximumNumberOfBackupsSelection = MaximumNumberOfBackups.FirstOrDefault(x => x.Value == SettingsController.CurrentSettings.MaximumNumberOfBackups);

        // Another app to start path
        AnotherAppToStartPath = SettingsController.CurrentSettings.AnotherAppToStartPath;

        // Api urls
        AlbionDataProjectBaseUrlWest = SettingsController.CurrentSettings.AlbionDataProjectBaseUrlWest;
        AlbionDataProjectBaseUrlEast = SettingsController.CurrentSettings.AlbionDataProjectBaseUrlEast;

        // Loot logger
        IsLootLoggerSaveReminderActive = SettingsController.CurrentSettings.IsLootLoggerSaveReminderActive;

        // Auto update
        IsSuggestPreReleaseUpdatesActive = SettingsController.CurrentSettings.IsSuggestPreReleaseUpdatesActive;

        // Item window
        IsOpenItemWindowInNewWindowChecked = SettingsController.CurrentSettings.IsOpenItemWindowInNewWindowChecked;

        // Packet Filter
        PacketFilter = SettingsController.CurrentSettings.PacketFilter;

        // Player Selection with same name in db
        PlayerSelectionWithSameNameInDb = SettingsController.CurrentSettings.ExactMatchPlayerNamesLineNumber;

        SetIconSourceToAnotherAppToStart();
    }

    public void SaveSettings()
    {
        SettingsController.CurrentSettings.Server = ServerSelection.Value;
        SettingsController.CurrentSettings.AnotherAppToStartPath = AnotherAppToStartPath;
        SettingsController.CurrentSettings.MaximumNumberOfBackups = MaximumNumberOfBackupsSelection.Value;
        SettingsController.CurrentSettings.BackupIntervalByDays = BackupIntervalByDaysSelection.Value;


        SettingsController.CurrentSettings.IsOpenItemWindowInNewWindowChecked = IsOpenItemWindowInNewWindowChecked;
        SettingsController.CurrentSettings.IsInfoWindowShownOnStart = ShowInfoWindowOnStartChecked;

        SettingsController.CurrentSettings.AlbionDataProjectBaseUrlWest = AlbionDataProjectBaseUrlWest;
        SettingsController.CurrentSettings.AlbionDataProjectBaseUrlEast = AlbionDataProjectBaseUrlEast;

        SettingsController.CurrentSettings.IsLootLoggerSaveReminderActive = IsLootLoggerSaveReminderActive;
        SettingsController.CurrentSettings.IsSuggestPreReleaseUpdatesActive = IsSuggestPreReleaseUpdatesActive;
        SettingsController.CurrentSettings.ExactMatchPlayerNamesLineNumber = PlayerSelectionWithSameNameInDb;

        SetNaviTabVisibilities();
        SetIconSourceToAnotherAppToStart();
    }

    public void ResetPacketFilter()
    {
        const string defaultFilter = "(host 5.45.187 or host 5.188.125) and udp port 5056";

        if (PacketFilter == defaultFilter) return;

        PacketFilter = defaultFilter;
    }

    public void ResetPlayerSelectionWithSameNameInDb()
    {
        if (PlayerSelectionWithSameNameInDb == 0) return;

        PlayerSelectionWithSameNameInDb = 0;
    }

    private void SetNaviTabVisibilities()
    {
        SettingsController.CurrentSettings.IsDashboardNaviTabActive = TabVisibilities?.FirstOrDefault(x => x?.NavigationTabFilterType == NavigationTabFilterType.Dashboard)?.IsSelected ?? true;
        SettingsController.CurrentSettings.IsItemSearchNaviTabActive = TabVisibilities?.FirstOrDefault(x => x?.NavigationTabFilterType == NavigationTabFilterType.ItemSearch)?.IsSelected ?? true;
        SettingsController.CurrentSettings.IsLoggingNaviTabActive = TabVisibilities?.FirstOrDefault(x => x?.NavigationTabFilterType == NavigationTabFilterType.Logging)?.IsSelected ?? true;
        SettingsController.CurrentSettings.IsDungeonsNaviTabActive = TabVisibilities?.FirstOrDefault(x => x?.NavigationTabFilterType == NavigationTabFilterType.Dungeons)?.IsSelected ?? true;
        SettingsController.CurrentSettings.IsDamageMeterNaviTabActive = TabVisibilities?.FirstOrDefault(x => x?.NavigationTabFilterType == NavigationTabFilterType.DamageMeter)?.IsSelected ?? true;
        SettingsController.CurrentSettings.IsTradeMonitoringNaviTabActive = TabVisibilities?.FirstOrDefault(x => x?.NavigationTabFilterType == NavigationTabFilterType.TradeMonitoring)?.IsSelected ?? true;
        SettingsController.CurrentSettings.IsGatheringNaviTabActive = TabVisibilities?.FirstOrDefault(x => x?.NavigationTabFilterType == NavigationTabFilterType.Gathering)?.IsSelected ?? true;
        SettingsController.CurrentSettings.IsPartyBuilderNaviTabActive = TabVisibilities?.FirstOrDefault(x => x?.NavigationTabFilterType == NavigationTabFilterType.PartyBuilder)?.IsSelected ?? true;
        SettingsController.CurrentSettings.IsStorageHistoryNaviTabActive = TabVisibilities?.FirstOrDefault(x => x?.NavigationTabFilterType == NavigationTabFilterType.StorageHistory)?.IsSelected ?? true;
        SettingsController.CurrentSettings.IsMapHistoryNaviTabActive = TabVisibilities?.FirstOrDefault(x => x?.NavigationTabFilterType == NavigationTabFilterType.MapHistory)?.IsSelected ?? true;
        SettingsController.CurrentSettings.IsPlayerInformationNaviTabActive = TabVisibilities?.FirstOrDefault(x => x?.NavigationTabFilterType == NavigationTabFilterType.PlayerInformation)?.IsSelected ?? true;

        var mainWindowViewModel = ServiceLocator.Resolve<MainWindowViewModel>();
        mainWindowViewModel.DashboardTabVisibility = SettingsController.CurrentSettings.IsDashboardNaviTabActive;
        mainWindowViewModel.ItemSearchTabVisibility = SettingsController.CurrentSettings.IsItemSearchNaviTabActive;
        mainWindowViewModel.LoggingTabVisibility = SettingsController.CurrentSettings.IsLoggingNaviTabActive;
        mainWindowViewModel.DungeonsTabVisibility = SettingsController.CurrentSettings.IsDungeonsNaviTabActive;
        mainWindowViewModel.DamageMeterTabVisibility = SettingsController.CurrentSettings.IsDamageMeterNaviTabActive;
        mainWindowViewModel.TradeMonitoringTabVisibility = SettingsController.CurrentSettings.IsTradeMonitoringNaviTabActive;
        mainWindowViewModel.GatheringTabVisibility = SettingsController.CurrentSettings.IsGatheringNaviTabActive;
        mainWindowViewModel.PartyBuilderTabVisibility = SettingsController.CurrentSettings.IsPartyBuilderNaviTabActive;
        mainWindowViewModel.StorageHistoryTabVisibility = SettingsController.CurrentSettings.IsStorageHistoryNaviTabActive;
        mainWindowViewModel.MapHistoryTabVisibility = SettingsController.CurrentSettings.IsMapHistoryNaviTabActive;
        mainWindowViewModel.PlayerInformationTabVisibility = SettingsController.CurrentSettings.IsPlayerInformationNaviTabActive;
    }

    private void SetIconSourceToAnotherAppToStart()
    {
        AnotherAppToStartExeIcon = GetExeIcon(SettingsController.CurrentSettings.AnotherAppToStartPath);
    }

    private static Bitmap? GetExeIcon(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                return null;
            }

            System.Drawing.Icon? appIcon = System.Drawing.Icon.ExtractAssociatedIcon(path);
            System.Drawing.Icon? highDpiIcon = appIcon;
            Bitmap imageResult;

            if (appIcon != null && appIcon.Handle != IntPtr.Zero)
            {
                highDpiIcon = System.Drawing.Icon.FromHandle(
                    new System.Drawing.Icon(appIcon, new System.Drawing.Size(64, 64)).Handle);
            }

            using (MemoryStream stream = new MemoryStream())
            {
                highDpiIcon?.Save(stream);

                stream.Seek(0, SeekOrigin.Begin);

                Bitmap bitmapImage = new Bitmap(stream);

                imageResult = bitmapImage;
            }

            highDpiIcon?.Dispose();
            appIcon?.Dispose();

            return imageResult;
        }
        catch
        {
            // ignore
        }

        return null;
    }

    public struct SettingDataInformation
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public struct SettingDataStringInformation
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    #region Inits

    private void InitServer()
    {
        Server.Clear();
        Server.Add(new SettingDataInformation { Name = "Auto"/*SettingsWindowTranslation.Automatically*/, Value = 0 });
        Server.Add(new SettingDataInformation { Name = "West"/*SettingsWindowTranslation.WestServer*/, Value = 1 });
        Server.Add(new SettingDataInformation { Name = "East"/*SettingsWindowTranslation.EastServer*/, Value = 2 });
        ServerSelection = Server.FirstOrDefault(x => x.Value == SettingsController.CurrentSettings.Server);
    }

    private static void InitDropDownDownByDays(ICollection<SettingDataInformation> updateJsonByDays)
    {
        updateJsonByDays.Clear();
        updateJsonByDays.Add(new SettingDataInformation { Name = "EVERY_DAY"/*LanguageController.Translation("EVERY_DAY")*/, Value = 1 });
        updateJsonByDays.Add(new SettingDataInformation { Name = "EVERY_3_DAYS"/*LanguageController.Translation("EVERY_3_DAYS")*/, Value = 3 });
        updateJsonByDays.Add(new SettingDataInformation { Name = "EVERY_7_DAYS"/*LanguageController.Translation("EVERY_7_DAYS")*/, Value = 7 });
        updateJsonByDays.Add(new SettingDataInformation { Name = "EVERY_14_DAYS"/*LanguageController.Translation("EVERY_14_DAYS")*/, Value = 14 });
        updateJsonByDays.Add(new SettingDataInformation { Name = "EVERY_28_DAYS"/*LanguageController.Translation("EVERY_28_DAYS")*/, Value = 28 });
    }

    private void InitMaxAmountOfBackups(ICollection<SettingDataInformation> amountOfBackups)
    {
        amountOfBackups.Clear();
        amountOfBackups.Add(new SettingDataInformation { Name = "1", Value = 1 });
        amountOfBackups.Add(new SettingDataInformation { Name = "3", Value = 3 });
        amountOfBackups.Add(new SettingDataInformation { Name = "5", Value = 5 });
        amountOfBackups.Add(new SettingDataInformation { Name = "10", Value = 10 });
        amountOfBackups.Add(new SettingDataInformation { Name = "20", Value = 20 });
        amountOfBackups.Add(new SettingDataInformation { Name = "50", Value = 50 });
        amountOfBackups.Add(new SettingDataInformation { Name = "100", Value = 100 });
    }

    private void InitNaviTabVisibilities()
    {
        TabVisibilities.Add(new TabVisibilityFilter(NavigationTabFilterType.Dashboard)
        {
            IsSelected = SettingsController.CurrentSettings.IsDashboardNaviTabActive,
            Name = "DASHBOARD"/*MainWindowTranslation.Dashnoard*/
        });
        TabVisibilities.Add(new TabVisibilityFilter(NavigationTabFilterType.ItemSearch)
        {
            IsSelected = SettingsController.CurrentSettings.IsItemSearchNaviTabActive,
            Name = "ITEM_SEARCH"/*MainWindowTranslation.ItemSearch*/
        });
        TabVisibilities.Add(new TabVisibilityFilter(NavigationTabFilterType.Logging)
        {
            IsSelected = SettingsController.CurrentSettings.IsLoggingNaviTabActive,
            Name = "LOGGING"/*MainWindowTranslation.Logging*/
        });
        TabVisibilities.Add(new TabVisibilityFilter(NavigationTabFilterType.Dungeons)
        {
            IsSelected = SettingsController.CurrentSettings.IsDungeonsNaviTabActive,
            Name = "DUNGEONS"/*MainWindowTranslation.Dungeons*/
        });
        TabVisibilities.Add(new TabVisibilityFilter(NavigationTabFilterType.DamageMeter)
        {
            IsSelected = SettingsController.CurrentSettings.IsDamageMeterNaviTabActive,
            Name = "DAMAGE_METER"/*MainWindowTranslation.DamageMeter*/
        });
        TabVisibilities.Add(new TabVisibilityFilter(NavigationTabFilterType.TradeMonitoring)
        {
            IsSelected = SettingsController.CurrentSettings.IsTradeMonitoringNaviTabActive,
            Name = "TRADE_MONITORING"/*MainWindowTranslation.TradeMonitoring*/
        });
        TabVisibilities.Add(new TabVisibilityFilter(NavigationTabFilterType.Gathering)
        {
            IsSelected = SettingsController.CurrentSettings.IsGatheringNaviTabActive,
            Name = "GATHERING"/*MainWindowTranslation.Gathering*/
        });
        TabVisibilities.Add(new TabVisibilityFilter(NavigationTabFilterType.PartyBuilder)
        {
            IsSelected = SettingsController.CurrentSettings.IsPartyBuilderNaviTabActive,
            Name = "PARTY_BUILDER"/*MainWindowTranslation.PartyBuilder*/
        });
        TabVisibilities.Add(new TabVisibilityFilter(NavigationTabFilterType.StorageHistory)
        {
            IsSelected = SettingsController.CurrentSettings.IsStorageHistoryNaviTabActive,
            Name = "STORAGE_HISTORY"/*MainWindowTranslation.StorageHistory*/
        });
        TabVisibilities.Add(new TabVisibilityFilter(NavigationTabFilterType.MapHistory)
        {
            IsSelected = SettingsController.CurrentSettings.IsMapHistoryNaviTabActive,
            Name = "MAP_HISTORY"/*MainWindowTranslation.MapHistory*/
        });
        TabVisibilities.Add(new TabVisibilityFilter(NavigationTabFilterType.PlayerInformation)
        {
            IsSelected = SettingsController.CurrentSettings.IsPlayerInformationNaviTabActive,
            Name = "PLAYER_INFORMATION"/*MainWindowTranslation.PlayerInformation*/
        });

        //Prevent the code below from running in design mode
        if (Design.IsDesignMode) return;

        var mainWindowViewModel = ServiceLocator.Resolve<MainWindowViewModel>();
        mainWindowViewModel.DashboardTabVisibility = SettingsController.CurrentSettings.IsDashboardNaviTabActive;
        mainWindowViewModel.ItemSearchTabVisibility = SettingsController.CurrentSettings.IsItemSearchNaviTabActive;
        mainWindowViewModel.LoggingTabVisibility = SettingsController.CurrentSettings.IsLoggingNaviTabActive;
        mainWindowViewModel.DungeonsTabVisibility = SettingsController.CurrentSettings.IsDungeonsNaviTabActive;
        mainWindowViewModel.DamageMeterTabVisibility = SettingsController.CurrentSettings.IsDamageMeterNaviTabActive;
        mainWindowViewModel.TradeMonitoringTabVisibility = SettingsController.CurrentSettings.IsTradeMonitoringNaviTabActive;
        mainWindowViewModel.GatheringTabVisibility = SettingsController.CurrentSettings.IsGatheringNaviTabActive;
        mainWindowViewModel.PartyBuilderTabVisibility = SettingsController.CurrentSettings.IsPartyBuilderNaviTabActive;
        mainWindowViewModel.StorageHistoryTabVisibility = SettingsController.CurrentSettings.IsStorageHistoryNaviTabActive;
        mainWindowViewModel.MapHistoryTabVisibility = SettingsController.CurrentSettings.IsMapHistoryNaviTabActive;
        mainWindowViewModel.PlayerInformationTabVisibility = SettingsController.CurrentSettings.IsPlayerInformationNaviTabActive;
    }

    #endregion
}
