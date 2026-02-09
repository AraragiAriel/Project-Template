using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public static class Extensions
{
#region INT

    public static IEnumerable<int> To(this int from, int to, bool forward = false){
        if(forward && from > to)
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

#endregion

#region FLOAT

    public static string ToPercent(this float f) => (f*100).ToString() + "%";

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

#endregion

#region ANIMATOR

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
}
