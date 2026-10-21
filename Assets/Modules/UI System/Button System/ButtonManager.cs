using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Button _button;
    private Button button => _button ??= GetComponent<Button>();

    public bool interactable
    {
        get => button.interactable;
        set => button.interactable = value;
    }
}
