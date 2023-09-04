using CommunityToolkit.Mvvm.ComponentModel;

namespace StatisticsAnalysisTool.Avalonia.ViewModels;
public partial class ToolLoadingWindowViewModel : ObservableObject
{
    [ObservableProperty] private double _progressBarValue;
}
