using CommunityToolkit.Mvvm.ComponentModel;
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

namespace StatisticsAnalysisTool.Avalonia.ViewModels;
public partial class SettingsWindowViewModel : ObservableObject
{
    #region Bindings

    [ObservableProperty] private string _packetFilter = string.Empty;
    [ObservableProperty] private string _anotherAppToStartPath = string.Empty;
    [ObservableProperty] private short _playerSelectionWithSameNameInDb = 0;
    [ObservableProperty] private bool _isOpenItemWindowInNewWindowChecked;
    [ObservableProperty] private bool _isLootLoggerSaveReminderActive;
    [ObservableProperty] private bool _isSuggestPreReleaseUpdatesActive;
    [ObservableProperty] private bool _isBackupNowButtonEnabled = true;
    [ObservableProperty] private bool _showInfoWindowOnStartChecked;
    [ObservableProperty] private Bitmap _anotherAppToStartExeIcon;
    [ObservableProperty] private SettingDataInformation _backupIntervalByDaysSelection;
    [ObservableProperty] private SettingDataInformation _maximumNumberOfBackupsSelection;
    [ObservableProperty] private SettingDataInformation _serverSelection;
    [ObservableProperty] private ObservableCollection<SettingDataInformation> _backupIntervalByDays = new();
    [ObservableProperty] private ObservableCollection<SettingDataInformation> _maximumNumberOfBackups = new();

    #endregion

    public SettingsWindowViewModel()
    {
        InitializeSetting();
    }

    public void InitializeSetting()
    {
        // Backup interval by days
        InitDropDownDownByDays(BackupIntervalByDays);
        BackupIntervalByDaysSelection = BackupIntervalByDays.FirstOrDefault(x => x.Value == SettingsController.CurrentSettings.BackupIntervalByDays);

        // Maximum number of backups
        InitMaxAmountOfBackups(MaximumNumberOfBackups);
        MaximumNumberOfBackupsSelection = MaximumNumberOfBackups.FirstOrDefault(x => x.Value == SettingsController.CurrentSettings.MaximumNumberOfBackups);

        // Another app to start path
        AnotherAppToStartPath = SettingsController.CurrentSettings.AnotherAppToStartPath;

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
        SettingsController.CurrentSettings.AnotherAppToStartPath = AnotherAppToStartPath;
        SettingsController.CurrentSettings.MaximumNumberOfBackups = MaximumNumberOfBackupsSelection.Value;
        SettingsController.CurrentSettings.BackupIntervalByDays = BackupIntervalByDaysSelection.Value;


        SettingsController.CurrentSettings.IsOpenItemWindowInNewWindowChecked = IsOpenItemWindowInNewWindowChecked;
        SettingsController.CurrentSettings.IsInfoWindowShownOnStart = ShowInfoWindowOnStartChecked;

        SettingsController.CurrentSettings.IsLootLoggerSaveReminderActive = IsLootLoggerSaveReminderActive;
        SettingsController.CurrentSettings.IsSuggestPreReleaseUpdatesActive = IsSuggestPreReleaseUpdatesActive;
        SettingsController.CurrentSettings.ExactMatchPlayerNamesLineNumber = PlayerSelectionWithSameNameInDb;

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

            System.Drawing.Icon appIcon = System.Drawing.Icon.ExtractAssociatedIcon(path);
            System.Drawing.Icon highDpiIcon = appIcon;
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

    #endregion
}
