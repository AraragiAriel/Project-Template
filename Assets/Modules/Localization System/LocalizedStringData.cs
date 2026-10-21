using UnityEngine;

[CreateAssetMenu(fileName = "LocalizedString ", menuName = "ScriptableObject/LocalizedStringData")]
public class LocalizedStringData : ScriptableObject, ILocalizer
{
    public LocalizedString localizedString;

    public LocalizedString Localize(string field = default) => localizedString;

    public static implicit operator string(LocalizedStringData data) =>
        data != null ? data.Localize(default) : "";
}