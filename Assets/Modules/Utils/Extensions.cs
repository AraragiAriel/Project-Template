using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Extensions
{
#region INT

    public static IEnumerable<int> To(this int from, int to, bool forward = false, bool backwards = false)
    {
        if(forward && from > to)
            yield break;

        if(backwards && from < to)
            yield break;

        int step = from <= to ? 1 : -1;
        for(int i = from; i != to + step; i += step)
            yield return i;
    }
    
    public static bool Even(this int i) => i % 2 == 0;
    public static bool Odd(this int i) => i % 2 == 1;
    public static int Loop(this int i, int change, int max, int min = 0){
        if(i + change > max)
            return min;
        if(i + change < min)
            return max;
        return i + change;
    }

    public static int Mod(this int f, int d)
    {
        int mod = f % d;
        return mod >= 0f ? mod : mod + d;
    }

#endregion

#region FLOAT

    public static Vector2 AngleToVector(this float angle)
        => new Vector2(Mathf.Cos(Mathf.Deg2Rad*angle), Mathf.Sin(Mathf.Deg2Rad*angle));
        
    public static float Mod(this float f, float d)
    {
        float mod = f % d;
        return mod >= 0f ? mod : mod + d;
    }

    public static string ToPercent(this float f) => (f*100).ToString() + "%";
    
    public static float ReScale(this float f, Vector2 oldScale, Vector2 newScale)
    {
        f = (f - oldScale.x)/oldScale.Range();
        f = f*newScale.Range() + newScale.x;
        return f;
    }

#endregion

#region BOOL

    public static float ToInt(this bool b) => b ? 1 : -1;

#endregion

#region VECTOR

    public static float Range(this Vector2 vector) => vector.y - vector.x;

#endregion

#region STRING

    public static string TagWrap(this string s, string tag)
    {
        if(string.IsNullOrEmpty(tag))
            return s;

        var strings = tag.Split('=');
        if(strings.Length < 2)
            return $"<{tag}>{s}</{tag}>";
        else
            return $"<{tag}>{s}</{strings[0]}>";
    }

    public static string ColorWrap(this string s, Color color)
        => TagWrap(s, $"color=#{ColorUtility.ToHtmlStringRGBA(color)}");
        
    public static string RemoveTags(this string s) => Regex.Replace(s, "<.*?>", string.Empty);

    public static string RemoveColorTags(this string s)
    => Regex.Replace
    (
        s,
        @"</?(?:color|myColor)(?:\s*=[^>]*)?>",
        string.Empty,
        RegexOptions.IgnoreCase
    );

#endregion

#region LIST

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

    public static bool TrimNull<T>(this List<T> list) => list.RemoveAll(
        i => i == null ||
        (i is Object obj && !obj)
    ) > 0;

    public static void RemoveRand<T>(this IList<T> list){
        list.RemoveAt(Random.Range(0, list.Count));
    }

    public static void RemoveTill<T>(this IList<T> list, int till){
        while(list.Count > till)
            list.RemoveRand();
    }

    public static T GetOrLast<T>(this IList<T> list, int n)
    {
        if(n < list.Count)
            return list[n];
            
        return list.Last();
    }
    
    public static void DestroyAll(this List<Object> objects)
    {
        int count = objects.Count;
        for(int i = count - 1; i >= 0; i--)
            objects[i].Destroy();
    }

    public static T PopFirst<T>(this IList<T> list) => list.Pop(0);
    public static T PopLast<T>(this IList<T> list) => list.Pop(list.Count - 1);
    public static T PopRand<T>(this IList<T> list) => list.Pop(Random.Range(0, list.Count - 1));
    public static T Pop<T>(this IList<T> list, int n){
        T t = list[n];
        list.Remove(t);
        return t;
    }

#endregion

#region COMPONENT

    public static bool TryGetComponentInChildren<T>(this Component component, out T output) where T : Component
    {
        output = component.GetComponentInChildren<T>();
        return output != null;
    }

    public static bool TryGetComponentInParent<T>(this Component component, out T output) where T : Component
    {
        output = component.GetComponentInParent<T>();
        return output != null;
    }

#endregion

#region ANIMATOR

    public static void PlayClip(this Animator animator, AnimationClip clip, bool dontRepeat = false){
        if(dontRepeat && animator.GetCurrentAnimatorStateInfo(0).IsName(clip.name)){
            return;
        }
        animator.Play(clip.name, 0, 0f);
    }

    public static void Randomize(this Animator anim){
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(stateInfo.fullPathHash, 0, Random.Range(0f, 1f));
    }

#endregion

#region GRADIENT

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

#endregion

#region  COLORS

    public static bool IsLight(this Color color)
    {
        Color.RGBToHSV(color, out var _, out var _, out var v);
        return v >= .5f;
    }

#endregion

#region OTHERS

    public static Rect GetScreenRect(this RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        Vector2 swCorner = CameraManager.instance.Get(CameraManager.Type.UI).WorldToScreenPoint(corners[0]);
        Vector2 neCorner = CameraManager.instance.Get(CameraManager.Type.UI).WorldToScreenPoint(corners[2]);
        return new Rect(swCorner, neCorner - swCorner);
    }

    public static void ToScreenPos(this RectTransform rt, Vector2 pos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rt.parent.GetComponent<RectTransform>(),
            pos,
            CameraManager.instance.Get(CameraManager.Type.UI),
            out Vector2 localPos
        );
        rt.anchoredPosition = localPos;
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

    public static IEnumerator StartCoroutines(this MonoBehaviour mb, params IEnumerator[] coroutines){
        int remaining = coroutines.Length;

        foreach(var coroutine in coroutines)
            mb.StartCoroutine(Wrap(coroutine));

        yield return new WaitUntil(() => remaining == 0 || !mb);

        IEnumerator Wrap(IEnumerator r){
            yield return r;
            remaining--;
        }
    }
    
    public static void Destroy(this UnityEngine.Object obj)
    {
        if(obj is GameObject && (obj as GameObject).TryGetComponent(out DontClear _))
            return;

        #if UNITY_EDITOR
            if(!EditorApplication.isPlaying)
                UnityEditor.Undo.DestroyObjectImmediate(obj);
            else
                GameObject.Destroy(obj);
        #else
            GameObject.Destroy(obj);
        #endif
    }

    public static void DestroyAllChildren(this Transform t)
    {
        for(int i = t.childCount - 1; i >= 0; i--)
            t.GetChild(i).gameObject.Destroy();
    }

#endregion
}
