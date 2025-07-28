using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private ClipData onSelectClip, onDeselectClip, onClickClip;
    public UnityEvent onSelect, onDeselect, onClick;

    public void OnPointerEnter(PointerEventData eventData){
        onSelect?.Invoke();
        AudioManager.PlayClip(onSelectClip);
    }

    public void OnPointerExit(PointerEventData eventData){
        onDeselect?.Invoke();
        AudioManager.PlayClip(onDeselectClip);
    }

    public void OnPointerClick(PointerEventData eventData){
        onClick?.Invoke();
        AudioManager.PlayClip(onClickClip);
    }
}
