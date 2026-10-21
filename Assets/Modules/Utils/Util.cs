using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Util
{
    public const int halfInspectorSpace = 8;
    public const int defaultInspectorSpace = 16;

    public static void Debug(string s)
        => UnityEngine.Debug.Log($"{s}\n\n[{UnityEngine.Random.Range(1,1000)}]");

    public static Vector2 AngleToVector(float angle)
    {
        return new Vector2(Mathf.Cos(Mathf.Deg2Rad*angle), Mathf.Sin(Mathf.Deg2Rad*angle));
    }
    
    public static float Round(float value, float multipleOf)
    {
        value /= multipleOf;
        value = Mathf.Round(value);
        value *= multipleOf;
        return value; 
    }
    
    public static Vector2 RotateVector2(Vector2 original, float angle){
        float rad = Mathf.Deg2Rad*angle;
        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);
        return new Vector2(cos*original.x - sin*original.y, sin*original.x + cos*original.y);
    }

    public static List<float> SplitSheetsString(string s){
        s = s.Replace(",", ".");
        var strings = Regex.Split(s, @"\s+");
        // var strings = s.Split(" ");
        List<float> floats = new();
        foreach(string single in strings){
            if(string.IsNullOrWhiteSpace(single))
                continue;
            floats.Add(float.Parse(single, CultureInfo.GetCultureInfo("en-US")));
        }
        return floats;
    }

    // Calcula decomposição de valor em quantidade de moedas
    public static List<int> ToDrop(List<int> values, int amount){
        List<int> toDrop = new();      
        for(int i = values.Count - 1; i >= 0; i--){
            int subTotal = amount/values[i];
            amount -= subTotal*values[i];
            foreach(int j in 1.To(subTotal))
                toDrop.Add(i);
        }
        if(amount > 0)
            UnityEngine.Debug.LogWarning("Couldn't drop full amount");
            
        return toDrop;
    }

    public static List<T> EnumList<T>() where T : Enum
    {
        var list = Enum.GetValues(typeof(T)).Cast<T>().ToList();
        foreach(var i in (list.Count - 1).To(0, backwards: true))
        {
            var field = typeof(T).GetField(list[i].ToString());
            if(field != null && Attribute.GetCustomAttribute(field, typeof(EnumSkipAttribute)) != null)
                list.RemoveAt(i);
        }
        return list;
    }

    public static Color AverageColor(List<Color> colors)
    {
        if(colors.Count == 0)
            return Color.white;

        var sum = Color.black;
        foreach(var color in colors)
            sum += color;
        
        return sum/colors.Count;
    }

    public static float MirroredFraction(this float f)
    {
        int times = Mathf.FloorToInt(f);
        float remainder = f % 1;
        return times % 2 == 0 ? remainder : 1f - remainder;
    }

    public static void DeselectPointerHandler(GameObject obj)
    {
        var handlers = obj.GetComponentsInChildren<IPointerExitHandler>();
        foreach(var handler in handlers)
        {
            handler.OnPointerExit(new(EventSystem.current));
        }
    }

    public static void SelectPointerHandler(GameObject obj)
    {
        var handlers = obj.GetComponentsInChildren<IPointerEnterHandler>();
        foreach(var handler in handlers)
        {
            handler.OnPointerEnter(new(EventSystem.current));
        }
    }

#region GAME SPECIFIC

#endregion

}