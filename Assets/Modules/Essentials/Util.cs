using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;
using System.Reflection;
using TMPro;



#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Util
{
    public const int halfInspectorSpace = 8;
    public const int defaultInspectorSpace = 16;

    public static void Debug(string s){
        UnityEngine.Debug.Log($"{s} | ({UnityEngine.Random.Range(1,1000)})");
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
        for(int i = count - 1; i >= 0; i--){
            #if UNITY_EDITOR
                if(!EditorApplication.isPlaying)
                    GameObject.DestroyImmediate(t.GetChild(i).gameObject);
                else
                    GameObject.Destroy(t.GetChild(i).gameObject);
            #else
                GameObject.Destroy(t.GetChild(i).gameObject);
            #endif
        }
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

    public static List<T> EnumList<T>() where T : System.Enum{
        return typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => !System.Attribute.IsDefined(f, typeof(EnumSkipAttribute)))
            .Select(f => (T)f.GetValue(null))
            .ToList();
    }

    #region FORMATTING

    public static string Concat(float value, bool allowDecimal = true, int digits = 3, bool round = true){
        if(digits < 1)
            return "0";

        value = SetDigits(value, digits, round);
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

    public static float SetDigits(float value, int digits, bool round = true){
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

    public static string FormatPercent(float value) => $"{SetDigits(value*100, 3).ToString(CultureInfo.InvariantCulture)}%";

    public static string ExposeSign(float value, bool addSpace = false){
        string s = "";
        s += value > 0f || Mathf.Approximately(value, 0f) ? "+" : "-";
        if(addSpace)
            s += " ";
        s += Mathf.Abs(value).ToString(CultureInfo.InvariantCulture);
        return s;
    }

    public static string ColorWrap(this string s, Color color)
        => $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{s}</color>";
        
    public static string TagWrap(this string s, string tag){
        if(string.IsNullOrEmpty(tag)) return s;
        return $"<{tag}>{s}</{tag}>";
    }

    #endregion

    #region RANDOM

    public static Vector2 randDir{
        get{
            float angle = UnityEngine.Random.Range(0f, 2*Mathf.PI);
            return new Vector2(
                Mathf.Cos(angle),
                Mathf.Sin(angle)
            );
        }
    }
    public static Vector2 randPos => randDir*UnityEngine.Random.Range(0f, 1f);
    public static int randSign => UnityEngine.Random.Range(0, 2) == 0 ?  1 : -1;
    public static bool randBool => ChanceCheck(.5f);
    public static bool ChanceCheck(float chance) => UnityEngine.Random.Range(0f, 1f) <= chance;
    public static float RandMult(float range) => UnityEngine.Random.Range(1f - range, 1f + range);
    public static float randMult => RandMult(.15f);
    public static int DrawWeightedIndex(List<float> chances){
        float sum = chances.Sum();
        if(sum <= 0f){
            UnityEngine.Debug.LogWarning("Total weighted chances not positive");
            return 0;
        }

        float partial = 0f;
        float rand = Random.Range(0f, sum);
        foreach(int i in 0.To(chances.Count - 1)){
            partial += chances[i];
            if(partial >= rand)
                return i;
        }
        UnityEngine.Debug.LogWarning("Couldn't draw position");
        return 0;
    }

    #endregion

    #region EXTENSIONS

    public static void Shuffle<T>(this IList<T> list){
        for(int i = 0; i < list.Count; i++){
            int id = UnityEngine.Random.Range(i, list.Count);
            (list[i], list[id]) = (list[id], list[i]);
        }
    }

    public static T Rand<T>(this IList<T> list){
        if(list.Count == 0) return default(T);
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static void TrimNull<T>(this IList<T> list){
        for(int i = list.Count - 1; i >= 0; i--)
            if(list[i] == null)
                list.RemoveAt(i);
    }

    public static void RemoveRand<T>(this IList<T> list){
        list.RemoveAt(Random.Range(0, list.Count));
    }

    public static void RemoveTill<T>(this IList<T> list, int till){
        while(list.Count > till)
            list.RemoveRand();
    }

    public static T PopFirst<T>(this IList<T> list) => list.Pop(0);
    public static T PopLast<T>(this IList<T> list) => list.Pop(list.Count - 1);
    public static T PopRand<T>(this IList<T> list) => list.Pop(Random.Range(0, list.Count - 1));
    public static T Pop<T>(this IList<T> list, int n){
        T t = list[n];
        list.Remove(t);
        return t;
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
    
    public static IEnumerable<int> To(this int from, int to){
        if(from < to)
            for(int i = from; i <= to; i++)
                yield return i;
        else
            for(int i = from; i >= to; i--)
                yield return i;
    }
    
    public static bool Even(this int i) => i % 2 == 0;
    public static bool Odd(this int i) => i % 2 == 1;
    public static int Next(this int i, int max) => i + 1 > max ? 0 : i +1;

    public static string ToPercent(this float f){
        return (f*100).ToString() + "%";
    }

    public static void SetGradient(this Gradient gradient, Color color1, Color color2) =>
        gradient.SetGradient(color1, color2, color1.a, color2.a);
    
    public static void SetGradient(this Gradient gradient, Color color, float alpha1, float alpha2) =>
        gradient.SetGradient(color, color, alpha1, alpha2);
    
    public static void SetGradient(this Gradient gradient, Color color) =>
        gradient.SetGradient(color, color, color.a, 0f);
    
    public static void SetGradient(this Gradient gradient, Color color1, Color color2, float alpha1, float alpha2){
        GradientColorKey[] colorKeys = new GradientColorKey[]{
            new GradientColorKey(color1, 0f),
            new GradientColorKey(color2, 1f),
        };
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[]{
            new GradientAlphaKey(alpha1 ,0f),
            new GradientAlphaKey(alpha2 ,1f),
        };
        gradient.SetKeys(colorKeys, alphaKeys);
    }

    public static void SetClip(this Animator animator, AnimationClip clip){
        if(animator.GetCurrentAnimatorStateInfo(0).IsName(clip.name)){
            return;
        }
        animator.Play(clip.name);
    }

    public static void Randomize(this Animator anim){
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(stateInfo.fullPathHash, 0, Random.Range(0f, 1f));
    }

    #endregion

    #region DATA TYPE

    public class BiDictionary<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>{
        private Dictionary<T1, T2> forward = new();
        private Dictionary<T2, T1> backward = new();

        public BiDictionary(){}

        public void Add(T1 key, T2 value){
            forward.Add(key, value);
            backward.Add(value, key);
        }

        public T2 Get(T1 key) => forward[key];
        public T1 Get(T2 key) => backward[key];

        public bool TryGet(T1 key, out T2 value) => forward.TryGetValue(key, out value);
        public bool TryGet(T2 key, out T1 value) => backward.TryGetValue(key, out value);

        public bool Remove(T1 key){
            if(forward.TryGetValue(key, out T2 val)){
                forward.Remove(key);
                backward.Remove(val);
                return true;
            }
            return false;
        }

        public bool Remove(T2 value){
            if(backward.TryGetValue(value, out T1 key)){
                backward.Remove(value);
                forward.Remove(key);
                return true;
            }
            return false;
        }   
        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator() => forward.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [System.Serializable]
    public class WeightedList<T>{
        [System.Serializable]
        public class Item<T1>{
            public T1 obj;
            public float weight;
        }
        public List<Item<T>> items;

        public T Draw(){
            int id = DrawWeightedIndex(items.Select(i => i.weight).ToList());
            return items[id].obj;
        }

        public WeightedList(){}

        public WeightedList(WeightedList<T> list){
            items = new(list.items);
        }
    }

    #endregion

    #region GAME SPECIFIC

    #endregion
}