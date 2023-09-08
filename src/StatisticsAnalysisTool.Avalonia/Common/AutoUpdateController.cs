using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using NetSparkleUpdater;
using NetSparkleUpdater.Enums;
using Serilog;
using StatisticsAnalysisTool.Avalonia.Properties;
using StatisticsAnalysisTool.Avalonia.Common.UserSettings;
using Application = Avalonia.Application;
using NetSparkleUpdater.SignatureVerifiers;

namespace StatisticsAnalysisTool.Avalonia.Common;
public static class AutoUpdateController
{
    public static async Task AutoUpdateAsync(bool reportErrors = false)
    {
        try
        {
            await HttpClientUtils.IsUrlAccessible(Settings.Default.AutoUpdatePreReleaseConfigUrl);
            await HttpClientUtils.IsUrlAccessible(Settings.Default.AutoUpdateConfigUrl);

            var sparkle = new SparkleUpdater(SettingsController.CurrentSettings.IsSuggestPreReleaseUpdatesActive
                ? Settings.Default.AutoUpdatePreReleaseConfigUrl
                : Settings.Default.AutoUpdateConfigUrl,
                new Ed25519Checker(SecurityMode.Unsafe))
            {
                UIFactory = new NetSparkleUpdater.UI.Avalonia.UIFactory(),
                RelaunchAfterUpdate = true,
                CustomInstallerArguments = "",
                TmpDownloadFilePath = Environment.CurrentDirectory,
                ShowsUIOnMainThread = true,
            };
            sparkle.StartLoop(true);

        }
        catch (HttpRequestException e)
        {
            Log.Warning(e, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
        }
        catch (Exception e)
        {
            Log.Error(e, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
        }
    }

    private static void AutoUpdateApplicationExit()
    {
        var desktop = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        desktop?.Shutdown();
    }
}
