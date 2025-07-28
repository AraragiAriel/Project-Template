using UnityEngine;

[CreateAssetMenu(fileName = "LocalizedString ", menuName = "ScriptableObject/LocalizedStringData")]
public class LocalizedStringData : ScriptableObject
{
    public LocalizedString localizedString;

    public string Localize(){
        return localizedString.Localize();
    }

    public static implicit operator string(LocalizedStringData data) =>
        data?.Localize();    
}