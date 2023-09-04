﻿using StatisticsAnalysisTool.Avalonia.Common.UserSettings;
using StatisticsAnalysisTool.Avalonia.Enumerations;
//using StatisticsAnalysisTool.Avalonia.Network.Manager;
using StatisticsAnalysisTool.Avalonia.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatisticsAnalysisTool.Avalonia.Common;

public class CriticalData
{
    private static SaveOnClosing _saveOnClosing;

    public static void Save()
    {
        Task.Run(SaveAsync).GetAwaiter().GetResult();
    }

    public static async Task SaveAsync()
    {
        if (_saveOnClosing is SaveOnClosing.IsRunning or SaveOnClosing.Done)
        {
            return;
        }

        _saveOnClosing = SaveOnClosing.IsRunning;

        //var trackingController = ServiceLocator.Resolve<TrackingController>();
        var mainWindowViewModel = ServiceLocator.Resolve<MainWindowViewModel>();

        var tasks = new List<Task>
        {
            Task.Run(SettingsController.SaveSettings),
            //Task.Run(mainWindowViewModel.SaveLootLogger),
            //Task.Run(async () => { await trackingController?.SaveDataAsync()!; })
        };

        await Task.WhenAll(tasks);

        _saveOnClosing = SaveOnClosing.Done;
    }

    public static SaveOnClosing GetStatus()
    {
        return _saveOnClosing;
    }
}