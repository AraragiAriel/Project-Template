using UnityEngine;

[CreateAssetMenu(fileName = "Color Data", menuName = "ScriptableObject/Others/ColorData")]
public class ColorData : ScriptableObject, IColor
{
    public Color color = Color.white;

    public Color GetColor() => color;

    public static implicit operator Color(ColorData colorData) =>
        colorData != null ? colorData.color : Color.white;
}
