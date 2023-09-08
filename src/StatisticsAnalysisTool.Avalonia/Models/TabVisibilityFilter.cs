using CommunityToolkit.Mvvm.ComponentModel;
using StatisticsAnalysisTool.Avalonia.Enumerations;

namespace StatisticsAnalysisTool.Avalonia.Models;

public partial class TabVisibilityFilter : ObservableObject
{
    [ObservableProperty] private bool? _isSelected;
    [ObservableProperty] private string? _name;

    public TabVisibilityFilter(NavigationTabFilterType navigationTabFilterType)
    {
        NavigationTabFilterType = navigationTabFilterType;
    }

    public NavigationTabFilterType NavigationTabFilterType { get; }
}