using System.Reflection;

namespace StatisticsAnalysisTool.Avalonia.ViewModels;
internal class FooterViewModel
{
    public static string Version => $"v{Assembly.GetExecutingAssembly().GetName().Version}";
}
