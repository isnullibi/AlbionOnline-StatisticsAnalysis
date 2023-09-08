using Avalonia.Controls;
using Avalonia.Interactivity;
using StatisticsAnalysisTool.Avalonia.ViewModels;

namespace StatisticsAnalysisTool.Avalonia.UserControls;

/// <summary>
/// Interaction logic for FooterControl.xaml
/// </summary>
public partial class FooterControl : UserControl
{
    public FooterControl()
    {
        InitializeComponent();
        var footerViewModel = new FooterViewModel();
        DataContext = footerViewModel;
    }

    private void Send(object sender, RoutedEventArgs e)
    {
        
    }
}