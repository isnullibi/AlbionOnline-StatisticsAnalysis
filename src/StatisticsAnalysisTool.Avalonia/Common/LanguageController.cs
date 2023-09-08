﻿using Serilog;
using StatisticsAnalysisTool.Avalonia.Common.UserSettings;
using StatisticsAnalysisTool.Avalonia.Models;
using StatisticsAnalysisTool.Avalonia.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace StatisticsAnalysisTool.Avalonia.Common;

public static class LanguageController
{
    private static readonly Dictionary<string, string> Translations = new();
    private static CultureInfo _currentCultureInfo;
    
    public static List<FileInformation> LanguageFiles { get; set; }

    static LanguageController()
    {
        //Dispatcher.UIThread.Invoke(() =>
        //{
            
        //});
    }

    public static CultureInfo CurrentCultureInfo
    {
        get => _currentCultureInfo;
        set
        {
            _currentCultureInfo = value;
            SettingsController.CurrentSettings.CurrentLanguageCultureName = value.TextInfo.CultureName;
            try
            {
                Thread.CurrentThread.CurrentUICulture = value;
                Thread.CurrentThread.CurrentCulture = value;
            }
            catch (Exception e)
            {
                //ConsoleManager.WriteLineForError(MethodBase.GetCurrentMethod()?.DeclaringType, e);
                Log.Error(e, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
            }
        }
    }

    public static bool InitializeLanguage()
    {
        try
        {
            if (CurrentCultureInfo == null)
            {
                if (!string.IsNullOrEmpty(SettingsController.CurrentSettings.CurrentLanguageCultureName))
                {
                    CurrentCultureInfo = new CultureInfo(SettingsController.CurrentSettings.CurrentLanguageCultureName);
                }
                else if (!string.IsNullOrEmpty(Settings.Default.DefaultLanguageCultureName))
                {
                    CurrentCultureInfo = new CultureInfo(Settings.Default.DefaultLanguageCultureName);
                }
                else
                {
                    throw new CultureNotFoundException();
                }
            }

            if (SetLanguage())
            {
                return true;
            }

            CurrentCultureInfo = new CultureInfo(Settings.Default.DefaultLanguageCultureName);
            if (SetLanguage())
            {
                return true;
            }

            throw new CultureNotFoundException();
        }
        catch (CultureNotFoundException e)
        {
            //ConsoleManager.WriteLineForError(MethodBase.GetCurrentMethod()?.DeclaringType, new CultureNotFoundException());
            Log.Error(e, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
            //MessageBox.Show("No culture info found!", Translation("ERROR"), MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
    }

    public static string Translation(string key, List<string> placeholders, List<string> replacements)
    {
        try
        {
            if (Translations.TryGetValue(key, out var value))
            {
                if (string.IsNullOrEmpty(value) || placeholders.Count != replacements.Count)
                {
                    return key;
                }

                for (int i = 0; i < placeholders.Count; i++)
                {
                    value = value.Replace("{" + placeholders[i] + "}", replacements[i]);
                }

                return value;
            }
        }
        catch (ArgumentNullException)
        {
            return "TRANSLATION-ERROR";
        }

        return key;
    }

    public static string Translation(string key)
    {
        try
        {
            if (Translations.TryGetValue(key, out var value))
            {
                return !string.IsNullOrEmpty(value) ? value : key;
            }
        }
        catch (ArgumentNullException)
        {
            return "TRANSLATION-ERROR";
        }

        return key;
    }

    public static bool SetLanguage()
    {
        InitializeLanguageFilesFromDirectory();

        try
        {
            if (LanguageFiles == null)
            {
                throw new FileNotFoundException();
            }

            var fileInfos = (from file in LanguageFiles
                             where file.FileName?.ToUpper() == CurrentCultureInfo?.TextInfo.CultureName.ToUpper()
                             select new FileInformation(file.FileName, file.FilePath)).FirstOrDefault();

            if (fileInfos == null)
            {
                return false;
            }

            if (!ReadAndAddLanguageFile(fileInfos.FilePath))
            {
                return false;
            }

            return true;
        }
        catch (ArgumentNullException e)
        {
            //MessageBox.Show(e.Message, Translation("ERROR"));
            //ConsoleManager.WriteLineForError(MethodBase.GetCurrentMethod()?.DeclaringType, e);
            Log.Error(e, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
            return false;
        }
        catch (FileNotFoundException ex)
        {
            //MessageBox.Show("Language file not found. ", Translation("ERROR"), MessageBoxButton.OK, MessageBoxImage.Error);
            //ConsoleManager.WriteLineForError(MethodBase.GetCurrentMethod()?.DeclaringType, ex);
            Log.Error(ex, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
            return false;
        }
    }

    private static bool ReadAndAddLanguageFile(string? filePath)
    {
        if (filePath is null) return false;

        try
        {
            Translations.Clear();
            var xmlReader = XmlReader.Create(filePath, new XmlReaderSettings()
            {
                Async = true
            });

            while (xmlReader.Read())
            {
                if (xmlReader.Name != "translation" || !xmlReader.HasAttributes)
                {
                    continue;
                }

                var translationLine = GetTranslationLine(xmlReader);

                if (string.IsNullOrEmpty(translationLine.Item1) || string.IsNullOrEmpty(translationLine.Item2))
                {
                    continue;
                }

                Translations.Add(translationLine.Item1, translationLine.Item2);
            }
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message, Translation("ERROR"));
            //ConsoleManager.WriteLineForError(MethodBase.GetCurrentMethod()?.DeclaringType, e);
            Log.Error(e, "{message}", MethodBase.GetCurrentMethod()?.DeclaringType);
            return false;
        }

        return true;
    }

    private static Tuple<string, string> GetTranslationLine(XmlReader xmlReader)
    {
        var name = xmlReader.GetAttribute("name");
        var value = xmlReader.ReadString();

        if (name == null || string.IsNullOrEmpty(value))
        {
            return new Tuple<string, string>(string.Empty, string.Empty);
        }

        if (Translations.ContainsKey(name))
        {
            Log.Warning("{message}", $"{MethodBase.GetCurrentMethod()?.DeclaringType}: {Translation("DOUBLE_VALUE_EXISTS_IN_THE_LANGUAGE_FILE")}: {name}");
            return new Tuple<string, string>(string.Empty, string.Empty);
        }

        return new Tuple<string, string>(name, value);
    }

    private static void InitializeLanguageFilesFromDirectory()
    {
        if (LanguageFiles != null)
        {
            return;
        }

        var languageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Settings.Default.LanguageDirectoryName);
        if (!Directory.Exists(languageFilePath))
        {
            return;
        }

        var files = DirectoryController.GetFiles(languageFilePath, "*.xml");
        if (files == null)
        {
            return;
        }

        LanguageFiles ??= new List<FileInformation>();

        foreach (var file in files)
        {
            var fileInfo = new FileInformation(Path.GetFileNameWithoutExtension(file), file);
            LanguageFiles.Add(fileInfo);
        }
    }

    public static void GetPercentageTranslationValues(string mainLanguageFileName = "en-US")
    {
        var mainLanguageFile = LanguageFiles.FirstOrDefault(x => string.Equals(x.FileName, mainLanguageFileName, StringComparison.CurrentCultureIgnoreCase));
        if (mainLanguageFile == null) return;
        
        var mainLanguageFileCount = CountTranslations(mainLanguageFile.FilePath);
        mainLanguageFile.PercentageTranslations = 100;

        foreach (FileInformation fileInformation in LanguageFiles.Where(x => x.FileName != mainLanguageFileName).ToList())
        {
            var countTranslations = CountTranslations(fileInformation.FilePath);
            double percentageValue = 100d / mainLanguageFileCount * countTranslations;

            var fileInfo = LanguageFiles.FirstOrDefault(x => x.FileName == fileInformation.FileName);
            if (fileInfo != null)
            {
                fileInfo.PercentageTranslations = (percentageValue > 100d) ? 100 : percentageValue;
            }
        }
    }

    private static int CountTranslations(string? filePath)
    {
        if (filePath == null) return 0;

        var xml = XDocument.Load(filePath);
        return xml.Descendants("translation").Count();
    }
}