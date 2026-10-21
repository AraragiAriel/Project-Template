using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    private Image _image;
    private Image image => _image ??= GetComponent<Image>();

    private TMP_Text _tmp;
    private TMP_Text tmp => _tmp ??= GetComponentInChildren<TMP_Text>();

    public void Set(string text, Color color, StringReplacer replacer = null)
    {
        image.color = color;
        tmp.Set(text.RemoveColorTags(), replacer);
        tmp.color = Res.data.colorTags.GetContrast(color);
    }
}
