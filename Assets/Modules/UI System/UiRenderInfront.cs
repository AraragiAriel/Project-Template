using UnityEngine;
using UnityEngine.UI;

public class UiRenderInfront : MonoBehaviour
{
    private Canvas canvas;
    private GraphicRaycaster raycaster;

    private void Awake(){
        if(canvas == null){
            canvas = gameObject.AddComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingLayerName = "UI";
            canvas.sortingOrder = 30;
        }
        if(raycaster == null)
            raycaster = gameObject.AddComponent<GraphicRaycaster>();
    }

    private void OnDestroy(){
        if(raycaster != null)
            Destroy(raycaster);
        if(canvas != null)
            Destroy(canvas);
    }
}
