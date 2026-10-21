using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(ButtonManager))]
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

    private ButtonManager _manager;
    private ButtonManager manager => _manager ??= GetComponent<ButtonManager>();

    public bool interactable => manager.interactable;

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