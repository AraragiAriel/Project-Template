using System;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Font Data", menuName = "ScriptableObject/Others/Font Data")]
public class FontData : ScriptableObject
{
    public TMP_FontAsset font;

#if UNITY_EDITOR
    public Action OnFontChanged;

    void OnValidate()
    {
        OnFontChanged?.Invoke();
    }
#endif
}
