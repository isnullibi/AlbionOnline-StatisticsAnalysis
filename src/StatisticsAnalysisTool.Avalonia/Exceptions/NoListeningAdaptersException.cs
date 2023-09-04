using System;

namespace StatisticsAnalysisTool.Avalonia.Exceptions;

public class NoListeningAdaptersException : Exception
{
    public NoListeningAdaptersException() : base("Error!\nThere are no listening adapters available!")
    {
    }
}