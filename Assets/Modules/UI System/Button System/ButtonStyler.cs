using System.Collections.Generic;
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

    private ButtonEvents _buttonEvents;
    private ButtonEvents buttonEvents => _buttonEvents ??= GetComponent<ButtonEvents>();

    private Transform _t;
    private Transform t => _t ??= transform;

    private Dictionary<Transform, Vector2> positions = new();

    void OnEnable()
    {
        buttonEvents.OnDown.AddListener(PointerDown);
        buttonEvents.OnUp.AddListener(PointerUp);
    }

    void OnDisable()
    {
        buttonEvents.OnDown.RemoveListener(PointerDown);
        buttonEvents.OnUp.RemoveListener(PointerUp);
    }

    private void PointerDown()
    {
        if(!data.move)
            return;

        positions.Clear();
        foreach(Transform child in t)
        {
            positions.Add(child, child.localPosition);
            child.localPosition += Vector3.down*data.pixels;
        }
    }

    private void PointerUp()
    {
        if(!data.move)
            return;
        
        foreach(Transform child in t)
        {
            if(positions.TryGetValue(child, out Vector2 pos))
                child.localPosition = pos;
        }
    }

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
