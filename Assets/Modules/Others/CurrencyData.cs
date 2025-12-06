using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "currency Data", menuName = "ScriptableObject/Currency Data")]
public class CurrencyData : ScriptableObject, IColor
{
    public Currency type;
    public Sprite icon;
    public LocalizedString description;
    public string textIcon;
    
    public ColorTag color;

    public ColorTag GetColor() => color;
}