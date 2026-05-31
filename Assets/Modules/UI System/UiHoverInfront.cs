using UnityEngine;
using UnityEngine.EventSystems;

public class UiHoverInfront : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UiRenderInfront render;

    public void OnPointerEnter(PointerEventData eventData){
        if(render == null)
            render = gameObject.AddComponent<UiRenderInfront>();
    }

    public void OnPointerExit(PointerEventData eventData){
        if(render != null)
            Destroy(render);
    }
}
