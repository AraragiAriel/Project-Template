using TMPro;
using Odin.OdinSerializer.Utilities;
using UnityEngine;

public static class TmpManager
{
    public static void Set(this TMP_Text tmp, string s, StringReplacer replacer = null)
    {
        if(!tmp.TryGetComponent(out TmpAnimator _))
            tmp.gameObject.AddComponent<TmpAnimator>();

        if(tmp.TryGetComponent(out TmpFixedTag fixedTag))
            foreach(var tag in fixedTag.tags)
                if(!tag.IsNullOrWhitespace())
                    s = s.TagWrap(tag);

        tmp.text = s.Parse(replacer);
    }

    public static string Parse(this string s, StringReplacer replacer = null)
    {
        int iterations = 0;
        while (true)
        {
            string starting = s;

            if (s.Contains("<loc="))
                s = Res.data.localizationData.Parse(s);

            if (replacer != null && s.Contains("{"))
                s = replacer.Replace(s);

            if (s.Contains("<myColor="))
                s = Res.data.colorTags.Parse(s);

            if(string.Equals(starting, s))
                break;

            iterations++;
            if(iterations > 50)
            {
                Debug.LogWarning($"Parsing reached {iterations} iterations");
                break;
            }
        }
        return s;
    }
}
