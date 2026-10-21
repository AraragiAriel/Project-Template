using UnityEngine;
using UnityEngine.EventSystems;

public class TweenUseOnHover : TweenUse, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(Tween(TweenData.Type.Forward));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(Tween(TweenData.Type.Reverse));
    }
}
