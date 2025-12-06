using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private ClipData onSelectClip, onDeselectClip, onClickClip;

    [Space]
    
    public UnityEvent onSelect;
    public UnityEvent onDeselect;
    public UnityEvent onLeftClick;
    public UnityEvent onRightClick;
    public UnityEvent onMiddleClick;
    public UnityEvent onAnyClick;

    private Button button;

    private void Awake(){
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData){
        if(!button.interactable) return;

        onSelect?.Invoke();
        AudioManager.Play(onSelectClip);
    }

    public void OnPointerExit(PointerEventData eventData){
        if(!button.interactable) return;
        
        onDeselect?.Invoke();
        AudioManager.Play(onDeselectClip);
    }

    public void OnPointerClick(PointerEventData eventData){
        if(!button.interactable) return;

        switch(eventData.button){
            case PointerEventData.InputButton.Left:
                onLeftClick?.Invoke();
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
