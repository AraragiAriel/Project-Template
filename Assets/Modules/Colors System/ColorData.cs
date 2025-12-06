using UnityEngine;

[CreateAssetMenu(fileName = "Color Data", menuName = "ScriptableObject/ColorData")]
public class ColorData : ScriptableObject, IColor
{
    public ColorTag color;

    public ColorTag GetColor() => color;

    public static implicit operator Color(ColorData colorData) =>
        colorData != null ? colorData.color : Color.white;
}
