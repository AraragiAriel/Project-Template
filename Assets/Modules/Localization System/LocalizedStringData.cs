using UnityEngine;

[CreateAssetMenu(fileName = "LocalizedString ", menuName = "ScriptableObject/LocalizedStringData")]
public class LocalizedStringData : ScriptableObject, ILocalizer
{
    public LocalizedString localizedString;
    public LocalizedString GetLocalizer() => localizedString;

    public string Localize(){
        return localizedString.Localize();
    }

    public static implicit operator string(LocalizedStringData data) =>
        data != null ? data.Localize() : "";
}