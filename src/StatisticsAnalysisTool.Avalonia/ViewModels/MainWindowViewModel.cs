using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace StatisticsAnalysisTool.Avalonia.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    #region Bindings

    [ObservableProperty] private string? _errorText;

    #endregion

    #region Error bar

    public void SetErrorBar(string? errorMessage)
    {
        ErrorText = errorMessage;
    }

    #endregion
}
