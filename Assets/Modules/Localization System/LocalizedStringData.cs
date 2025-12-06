using UnityEngine;

[CreateAssetMenu(fileName = "LocalizedString ", menuName = "ScriptableObject/LocalizedStringData")]
public class LocalizedStringData : ScriptableObject, ILocalizer
{
    public LocalizedString localizedString;

    public string Localize(string field) => Localize();
    public string Localize() => localizedString.Localize();

    public static implicit operator string(LocalizedStringData data) =>
        data != null ? data.Localize() : "";
}