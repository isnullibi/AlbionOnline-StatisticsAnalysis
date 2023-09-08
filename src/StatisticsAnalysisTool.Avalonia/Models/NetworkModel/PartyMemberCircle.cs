using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace StatisticsAnalysisTool.Avalonia.Models.NetworkModel;
public partial class PartyMemberCircle : ObservableObject
{
    public Guid UserGuid { get; set; }

    [ObservableProperty] private string? _name;
}
