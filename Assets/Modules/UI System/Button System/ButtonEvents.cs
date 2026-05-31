using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonEvents : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Clips")]
    [SerializeField] private ClipData onSelectClip;
    [SerializeField] private ClipData onDeselectClip;
    [SerializeField] private ClipData onClickClip;
    [SerializeField] private ClipData onMisclickClip;

    [Header("Events")]
    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;
    public UnityEvent OnLeftClick;
    public UnityEvent OnRightClick;
    public UnityEvent OnMiddleClick;
    public UnityEvent OnAnyClick;
    public UnityEvent OnDown;
    public UnityEvent OnUp;

    public bool interactable
    {
        get => button.interactable;
        set => button.interactable = value;
    }

    private Button _button;
    private Button button => _button ??= GetComponent<Button>();

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!interactable)
            return;

        OnSelect?.Invoke();
        AudioManager.Play(onSelectClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!interactable)
            return;
        
        OnDeselect?.Invoke();
        AudioManager.Play(onDeselectClip);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!interactable)
        {
            AudioManager.Play(onMisclickClip);
            return;
        }

        switch(eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnLeftClick?.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                OnRightClick?.Invoke();
                break;
            case PointerEventData.InputButton.Middle:
                OnMiddleClick?.Invoke();
                break;
        }
        OnAnyClick?.Invoke();
        
        AudioManager.Play(onClickClip);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!interactable)
            return;

        OnDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!interactable)
            return;

        OnUp?.Invoke();
    }
}