using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class StringReplacer
{
    private Dictionary<string, string> placeholders = new();
    public void Add(string key, string value)
    {
        if(placeholders.ContainsKey(key))
            placeholders[key] = value;
        else
            placeholders.Add(key, value);
    }

    private static readonly Regex regex =
        new(@"\{([^{}]*)\}");

    public string Replace(string s)
    {
        return regex.Replace(s, match =>
        {
            string key = match.Groups[1].Value;

            return placeholders.TryGetValue(key, out string value)
                ? value
                : match.Value;
        });
    }
}
