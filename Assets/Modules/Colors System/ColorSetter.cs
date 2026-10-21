using UnityEngine;

[RequireComponent(typeof(ColorApply))]
public class ColorSetter : MonoBehaviour
{
    [SerializeField] private ColorTag color;

    private ColorApply _colorApply;
    private ColorApply colorApply => _colorApply ??= GetComponent<ColorApply>();

    void OnEnable()
    {
        colorApply.Set(color);
    }
}
