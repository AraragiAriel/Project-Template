using UnityEngine;

[System.Serializable]
public class ColorFlex
{
    public string tag;
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private ScriptableObject data;

    public Color color{
        get{
            if(data != null && data is IColor icolor)
                return icolor.GetColor();
            return _color;
        }
    }

    public static implicit operator Color(ColorFlex colorFlex) =>
        colorFlex != null ? colorFlex.color : Color.white;

    public void OnValidate(){
        if(!(data is IColor))
            data = null;
        if(data != null)
            _color = color;
    }
}
