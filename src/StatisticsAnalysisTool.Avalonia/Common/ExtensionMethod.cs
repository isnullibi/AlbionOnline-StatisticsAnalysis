using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace StatisticsAnalysisTool.Avalonia.Common;

public static class ExtensionMethod
{
    public static string SetYesOrNo(this bool value)
    {
        return (value) ? LanguageController.Translation("YES") : LanguageController.Translation("NO");
    }

    public static void OrderByReference<T>(this ObservableCollection<T> collection, List<T> comparison)
    {
        for (var i = 0; i < comparison.Count; i++)
        {
            if (!comparison.ElementAt(i).Equals(collection.ElementAt(i)))
            {
                collection.Move(collection.IndexOf(comparison[i]), i);
            }
        }
    }

    public static Dictionary<int, T> ToDictionary<T>(this IEnumerable<T> array)
    {
        return array
            .Select((v, i) => new { Key = i, Value = v })
            .ToDictionary(o => o.Key, o => o.Value);
    }
    public static string ToTimerString(this TimeSpan span)
    {
        return $"{span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}";
    }

    public static string ToTimerString(this int seconds)
    {
        var span = new TimeSpan(0, 0, 0, seconds);
        return $"{span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}";
    }

    #region Json

    public static async Task<string> SerializeJsonStringAsync(this object obj, JsonSerializerOptions? option = null)
    {
        using var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, obj, obj.GetType(), option);
        stream.Position = 0;
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }

    #endregion

    #region Player Objects

    //public static long GetCurrentTotalDamage(this List<KeyValuePair<Guid, PlayerGameObject>> playerObjects)
    //{
    //    return playerObjects.Count <= 0 ? 0 : playerObjects.Max(x => x.Value.Damage);
    //}

    //public static long GetCurrentTotalHeal(this List<KeyValuePair<Guid, PlayerGameObject>> playerObjects)
    //{
    //    return playerObjects.Count <= 0 ? 0 : playerObjects.Max(x => x.Value.Heal);
    //}

    //public static double GetDamagePercentage(this List<KeyValuePair<Guid, PlayerGameObject>> playerObjects, double playerDamage)
    //{
    //    var totalDamage = playerObjects.Sum(x => x.Value.Damage);
    //    return 100.00 / totalDamage * playerDamage;
    //}

    //public static double GetHealPercentage(this List<KeyValuePair<Guid, PlayerGameObject>> playerObjects, double playerHeal)
    //{
    //    var totalHeal = playerObjects.Sum(x => x.Value.Heal);
    //    return 100.00 / totalHeal * playerHeal;
    //}

    #endregion

    #region Lists / Arrays

    public static bool IsInBounds<T>(this IEnumerable<T> array, long index)
    {
        return index >= 0 && index < array.Count() - 1;
    }

    #endregion
}
