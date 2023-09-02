using StatisticsAnalysisTool.Avalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;

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