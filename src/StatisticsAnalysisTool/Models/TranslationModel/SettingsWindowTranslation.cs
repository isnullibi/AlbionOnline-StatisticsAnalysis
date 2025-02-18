﻿using StatisticsAnalysisTool.Localization;

namespace StatisticsAnalysisTool.Models.TranslationModel;

public class SettingsWindowTranslation
{
    public static string Settings => LanguageController.Translation("SETTINGS");
    public static string Language => $"{LanguageController.Translation("LANGUAGE")} ({LanguageController.Translation("PROGRAM_RESTART_REQUIRED")})";
    public static string CheckForUpdate => LanguageController.Translation("CHECK_FOR_UPDATE");
    public static string RefreshRate => LanguageController.Translation("REFRESH_RATE");
    public static string SetServerManually => LanguageController.Translation("SET_SERVER_MANUALLY");
    public static string NetworkFiltering => LanguageController.Translation("NETWORK_FILTERING");
    public static string OpenItemWindowInNewWindow => LanguageController.Translation("OPEN_ITEM_WINDOW_IN_NEW_WINDOW");
    public static string ShowInfoWindowOnStart => LanguageController.Translation("SHOW_INFO_WINDOW_ON_START");
    public static string Save => LanguageController.Translation("SAVE");
    public static string AlarmSoundUsed => LanguageController.Translation("ALARM_SOUND_USED");
    public static string ToolDirectory => LanguageController.Translation("TOOL_DIRECTORY");
    public static string OpenToolDirectory => LanguageController.Translation("OPEN_TOOL_DIRECTORY");
    public static string OpenDebugConsole => LanguageController.Translation("OPEN_DEBUG_CONSOLE");
    public static string CreateDesktopShortcut => LanguageController.Translation("CREATE_DESKTOP_SHORTCUT");
    public static string AlbionDataProjectBaseUrlWest => LanguageController.Translation("ALBION_DATA_PROJECT_BASE_URL_WEST");
    public static string AlbionDataProjectBaseUrlEast => LanguageController.Translation("ALBION_DATA_PROJECT_BASE_URL_EAST");
    public static string GoldStatsApiUrl => LanguageController.Translation("GOLD_STATS_API_URL");
    public static string IsLootLoggerSaveReminderActive => LanguageController.Translation("IS_LOOT_LOGGER_SAVE_REMINDER_ACTIVE");
    public static string ExportLootLoggingFileWithRealItemName => LanguageController.Translation("EXPORT_LOOT_LOGGING_FILE_WITH_REAL_ITEM_NAME");
    public static string FiveSeconds => LanguageController.Translation("5_SECONDS");
    public static string TenSeconds => LanguageController.Translation("10_SECONDS");
    public static string ThirtySeconds => LanguageController.Translation("30_SECONDS");
    public static string SixtySeconds => LanguageController.Translation("60_SECONDS");
    public static string FiveMinutes => LanguageController.Translation("5_MINUTES");
    public static string SuggestPreReleaseUpdates => LanguageController.Translation("SUGGEST_PRE_RELEASE_UPDATES");
    public static string AttentionTheseVersionsAreStillBeingTested => LanguageController.Translation("ATTENTION_THESE_VERSION_ARE_STILL_BEING_TESTED");
    public static string CharacterNameToTrack => LanguageController.Translation("CHARACTER_NAME_TO_TRACK");
    public static string NavigationTabVisibility => LanguageController.Translation("NAVIGATION_TAB_VISIBILITY");
    public static string Automatically => LanguageController.Translation("AUTOMATICALLY");
    public static string WestServer => LanguageController.Translation("WEST_SERVER");
    public static string EastServer => LanguageController.Translation("EAST_SERVER");
    public static string Activated => LanguageController.Translation("ACTIVATED");
    public static string Disabled => LanguageController.Translation("DISABLED");
    public static string Notifications => LanguageController.Translation("NOTIFICATIONS");
    public static string PacketFilter => LanguageController.Translation("PACKET_FILTER");
    public static string Reset => LanguageController.Translation("RESET");
    public static string PlayerSelectionWithSameNameInDb => LanguageController.Translation("PLAYER_SELECTION_WITH_SAME_NAME_IN_DB");
    public static string UpdateNow => LanguageController.Translation("UPDATE_NOW");
    public static string BackupInterval => LanguageController.Translation("BACKUP_INTERVAL");
    public static string BackupNow => LanguageController.Translation("BACKUP_NOW");
    public static string MaximumNumberOfBackups => LanguageController.Translation("MAXIMUM_NUMBER_OF_BACKUPS");
    public static string Generally => LanguageController.Translation("GENERALLY");
    public static string Backup => LanguageController.Translation("BACKUP");
    public static string Tool => LanguageController.Translation("TOOL");
    public static string Tracking => LanguageController.Translation("TRACKING");
    public static string ItemWindow => LanguageController.Translation("ITEM_WINDOW");
    public static string ItemSearch => LanguageController.Translation("ITEM_SEARCH");
    public static string LootLogger => LanguageController.Translation("LOOT_LOGGER");
    public static string DamageMeter => LanguageController.Translation("DAMAGE_METER");
    public static string AnotherAppToStartPath => LanguageController.Translation("ANOTHER_APP_TO_START_PATH");
}