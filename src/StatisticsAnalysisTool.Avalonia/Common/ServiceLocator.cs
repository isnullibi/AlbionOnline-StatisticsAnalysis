﻿using System;
using System.Collections.Generic;

namespace StatisticsAnalysisTool.Avalonia.Common;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> Services = new ();

    public static void Register<T>(object service)
    {
        Services[typeof(T)] = service;
    }

    public static T Resolve<T>()
    {
        return (T) Services[typeof(T)];
    }
}