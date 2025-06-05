using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public static class Utilities
{
    public const int halfInspectorSpace = 8;
    public const int defaultInspectorSpace = 16;

    public static Vector2 randomDir{
        get{
            float angle = UnityEngine.Random.Range(0f, 2*Mathf.PI);
            return new Vector2(
                Mathf.Cos(angle),
                Mathf.Sin(angle)
            );
        }
    }
    public static Vector2 randomPos => randomDir*UnityEngine.Random.Range(0f, 1f);
    public static int randomSign => UnityEngine.Random.Range(0, 2) == 0 ?  1 : -1;

    public static void DebugWithRand(string s){
        Debug.Log(s + " | (" + UnityEngine.Random.Range(1,100) + ")");
    }

    public static Vector2 AngleToVector(float angle){
        return new Vector2(Mathf.Cos(Mathf.Deg2Rad*angle), Mathf.Sin(Mathf.Deg2Rad*angle));
    }
    
    public static float Round(float value, float multipleOf){
        value /= multipleOf;
        value = Mathf.Round(value);
        value *= multipleOf;
        return value; 
    }

    public static void DestroyAllChildren(Transform t){
        int count = t.childCount;
        for(int i = count - 1; i >= 0; i--)
            GameObject.Destroy(t.GetChild(i).gameObject);
    }

    public static void DestroyAllChildrenImmediately(Transform t){
        int count = t.childCount;
        for(int i = count - 1; i >= 0; i--)
            GameObject.DestroyImmediate(t.GetChild(i).gameObject);
    }

    public static string Concat(float value, bool allowDecimal, bool round = true){
        value = SetDigits(value, 3, round);
        string format = "0.##";

        string[] suffixes = {"", "K", "M", "B", "T"};
        int suffixId = 0;

        float abs = Mathf.Abs(value);
        while(abs >= 1000f && suffixId < suffixes.Length - 1){
            abs /= 1000;
            suffixId++;
        }

        if(suffixId == 0 && !allowDecimal){
            if(!round)
                abs = Mathf.Floor(abs);
            format = "F0";
        }


        float result = Mathf.Sign(value)*abs;
        return result.ToString(format, CultureInfo.InvariantCulture) + suffixes[suffixId];
    }

    public static float SetDigits(float value, int digits, bool round){
        if(value == 0)
            return 0f;

        int power = Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(value)));
        float scale = Mathf.Pow(10, digits - 1 - power);
        float truncated;
        if(round)
            truncated = Mathf.Round(value*scale)/scale;
        else
            truncated = Mathf.Floor(value*scale)/scale;

        return truncated;
    }
    
    public static Vector2 RotateVector2(Vector2 original, float angle){
        float rad = Mathf.Deg2Rad*angle;
        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);
        return new Vector2(cos*original.x - sin*original.y, sin*original.x + cos*original.y);
    }

    public static void ClearNull<T>(this IList<T> list){
        for(int i = list.Count - 1; i >= 0; i--)
            if(list[i] == null)
                list.RemoveAt(i);
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

    // EXTENSIONS

    public static void Shuffle<T>(this IList<T> list){
        for(int i = 0; i < list.Count; i++){
            int id = UnityEngine.Random.Range(i, list.Count);
            (list[i], list[id]) = (list[id], list[i]);
        }
    }

    public static T Rand<T>(this IList<T> list){
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static void SetAlpha(this Image image, float alpha){
        var aux = image.color;
        aux.a = alpha;
        image.color = aux;
    }

    public static void SetAlpha(this SpriteRenderer sr, float alpha){
        var aux = sr.color;
        aux.a = alpha;
        sr.color = aux;
    }
}
