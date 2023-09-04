using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Projektanker.Icons.Avalonia;

namespace StatisticsAnalysisTool.Avalonia.UserControls;

public partial class ErrorBarControl : UserControl
{
    public static readonly StyledProperty<string?> ErrorTextProperty =
        AvaloniaProperty.Register<ErrorBarControl, string?>(nameof(ErrorText));
    
    public string? ErrorText
    {
        get => GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    public ErrorBarControl()
    {
        ErrorTextProperty.Changed.Subscribe(ErrorTextPropertyChanged);
        InitializeComponent();
    }

    private void ErrorTextPropertyChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue?.ToString() != string.Empty) IsVisible = true;
        else IsVisible = false;
    }

    private void BtnErrorBar_Click(object sender, RoutedEventArgs e)
    {
        ErrorText = string.Empty;
    }
}