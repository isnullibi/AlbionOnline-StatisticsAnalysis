using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HarfBuzzSharp;
using Projektanker.Icons.Avalonia;

namespace StatisticsAnalysisTool.Avalonia.Common;
public class ScrollViewerExtensions
{
    public static readonly AttachedProperty<bool> AlwaysScrollToEndProperty =
        AvaloniaProperty.RegisterAttached<ScrollViewerExtensions, Control, bool>("AlwaysScrollToEnd");

    private static bool _autoScroll;

    static ScrollViewerExtensions()
    {
        AlwaysScrollToEndProperty.Changed.Subscribe(AlwaysScrollToEndChanged);
    }

    private static void AlwaysScrollToEndChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is ScrollViewer scroll)
        {
            if (e.NewValue != null && (bool) e.NewValue)
            {
                scroll.ScrollToEnd();
                scroll.ScrollChanged += ScrollChanged;
            }
            else scroll.ScrollChanged -= ScrollChanged;
        }
        else throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");

    }

    public static bool GetAlwaysScrollToEnd(ScrollViewer scroll)
    {
        if (scroll == null) throw new ArgumentNullException(nameof(scroll));

        return scroll.GetValue(AlwaysScrollToEndProperty);
    }

    public static void SetAlwaysScrollToEnd(ScrollViewer scroll, bool value)
    {
        if (scroll == null) throw new ArgumentNullException(nameof(scroll));

        scroll.SetValue(AlwaysScrollToEndProperty, value);
    }

    private static void ScrollChanged(object? sender, ScrollChangedEventArgs e)
    {        
        if (sender is not ScrollViewer scroll)
        {
            throw new InvalidOperationException("The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");
        }

        if (e.ExtentDelta.Y == 0)
        {
            _autoScroll = Math.Abs(scroll.Offset.Y - scroll.ScrollBarMaximum.Y) < 0;
        }

        if (_autoScroll && e.ExtentDelta.Y != 0)
        {
            scroll.ScrollToEnd();
        }
    }
}
