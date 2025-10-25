using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private ClipData onSelectClip, onDeselectClip, onClickClip;

    [Space]
    
    public UnityEvent onSelect, onDeselect, onClick;
    private Button button;

    private void Awake(){
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData){
        if(!button.interactable) return;

        onSelect?.Invoke();
        AudioManager.PlayClip(onSelectClip);
    }

    public void OnPointerExit(PointerEventData eventData){
        if(!button.interactable) return;
        
        onDeselect?.Invoke();
        AudioManager.PlayClip(onDeselectClip);
    }

    public void OnPointerClick(PointerEventData eventData){
        if(!button.interactable) return;
        
        onClick?.Invoke();
        AudioManager.PlayClip(onClickClip);
    }
}
