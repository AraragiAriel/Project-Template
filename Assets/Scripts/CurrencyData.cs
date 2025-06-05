using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "currency Data", menuName = "ScriptableObject/Others/Currency Data")]
public class CurrencyData : ScriptableObject
{
    public Currency type;
    public Sprite icon;
    public LocalizedString description;
    public string textIcon;
    
    public Color color{
        get => Color.white;
    }
}