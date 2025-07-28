using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public static class Util
{
    public const int halfInspectorSpace = 8;
    public const int defaultInspectorSpace = 16;

    public static void Debug(string s){
        UnityEngine.Debug.Log(s + " | (" + UnityEngine.Random.Range(1,100) + ")");
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
    public static bool randBool => RandBool(.5f);
    public static bool RandBool(float chance) => UnityEngine.Random.Range(0f, 1f) <= chance;
    public static float RandMult(float range) => UnityEngine.Random.Range(1f - range, 1f + range);
    public static float randMult => RandMult(.25f);

    #endregion

    #region EXTENSIONS

    public static void Shuffle<T>(this IList<T> list){
        for(int i = 0; i < list.Count; i++){
            int id = UnityEngine.Random.Range(i, list.Count);
            (list[i], list[id]) = (list[id], list[i]);
        }
    }

    public static T Rand<T>(this IList<T> list){
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static void TrimNull<T>(this IList<T> list){
        for(int i = list.Count - 1; i >= 0; i--)
            if(list[i] == null)
                list.RemoveAt(i);
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
    
    public static IEnumerable<int> To(this int min, int max) {
        for(int i = min; i <= max; i++)
            yield return i;
    }
    
    public static bool Even(this int i) => i % 2 == 0;
    public static bool Odd (this int i) => i % 2 == 1;

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

    #endregion

    #region GAME SPECIFIC

    #endregion
}
