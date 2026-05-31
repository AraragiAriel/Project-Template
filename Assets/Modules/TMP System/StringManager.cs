using UnityEngine;
using TMPro;
using Odin.OdinSerializer.Utilities;

public static class StringManager
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
        if(replacer != null)
            s = replacer.Replace(s);
        s = Res.data.localizationData.Parse(s);
        s = Res.data.colorTags.Parse(s);
        return s;
    }
}
