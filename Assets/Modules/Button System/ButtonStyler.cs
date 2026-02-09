using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonStyler : MonoBehaviour
{
    public ButtonStyleData data;

    private Button _button;
    private Button button => _button ??= GetComponent<Button>();

    private Image _image;
    private Image image => _image ??= GetComponent<Image>();

    public void ApplyStyle()
    {
        if(data == null) return;

        image.sprite = data.sprite;

        button.transition = data.transition;
        button.colors = data.colors;
        button.spriteState = data.spriteState;
        button.animationTriggers = data.animationTriggers;
    }

    public void ReadStyle()
    {
        if(data == null) return;

        data.sprite = image.sprite;

        data.transition = button.transition;
        data.colors = button.colors;
        data.spriteState = button.spriteState;
        data.animationTriggers = button.animationTriggers;
    }
}
