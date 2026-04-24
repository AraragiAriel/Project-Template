using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Clips")]
    [SerializeField] private ClipData onSelectClip;
    [SerializeField] private ClipData onDeselectClip;
    [SerializeField] private ClipData onClickClip;

    [Header("Events")]
    public UnityEvent onSelect;
    public UnityEvent onDeselect;
    public UnityEvent onClick;
    public UnityEvent onRightClick;
    public UnityEvent onMiddleClick;
    public UnityEvent onAnyClick;

    public bool interactable
    {
        get => button.interactable;
        set => button.interactable = value;
    }

    private Button _button;
    private Button button => _button ??= GetComponent<Button>();

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!button.interactable) return;

        onSelect?.Invoke();
        AudioManager.Play(onSelectClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!button.interactable) return;
        
        onDeselect?.Invoke();
        AudioManager.Play(onDeselectClip);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!button.interactable) return;

        switch(eventData.button)
        {
            case PointerEventData.InputButton.Left:
                onClick?.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                onRightClick?.Invoke();
                break;
            case PointerEventData.InputButton.Middle:
                onMiddleClick?.Invoke();
                break;
        }
        onAnyClick?.Invoke();
        
        AudioManager.Play(onClickClip);
    }
}