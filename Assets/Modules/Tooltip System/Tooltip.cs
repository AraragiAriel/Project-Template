using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

public class Tooltip : MonoBehaviour
{
    private const float spacingMult = .01f;
    private float spacing => Screen.width*spacingMult;

    private RectTransform _tooltipRect;
    public RectTransform tooltipRect => _tooltipRect ??= transform.GetChild(0).GetComponent<RectTransform>();
    
    private CanvasGroup _canvasGroup;
    private CanvasGroup canvasGroup => _canvasGroup ??= GetComponent<CanvasGroup>();

    private const float duration = .25f;
    private const Ease ease = Ease.OutSine;
    
    protected TooltipData data;

    public void Set(TooltipData data){
        // SETUP
        canvasGroup.alpha = 0f;
        this.data = data;
        if(TryGetComponent(out TooltipSetup setup))
        {
            setup.Setup(data);
        }
        Canvas.ForceUpdateCanvases();

        StartCoroutine(SetPosition());
    }

    private IEnumerator SetPosition()
    {
        yield return new WaitForEndOfFrame();

        var thisRect = GetRect(tooltipRect);
        var rects = data.rects.Select(rect => GetRect(rect)).ToList();
        Vector2 finalPos = Vector2.zero;
        // Util.Debug($"this rect: [{thisRect.width}; {thisRect.height}]");
        foreach(int i in (rects.Count - 1).To(0)){
            bool found = false;

            float xOffset = (rects[i].width + thisRect.width)/2f + spacing;
            float yOffset = (rects[i].height + thisRect.height)/2f + spacing;

            List<Vector2> offsets = new(){
                new Vector2(0f, yOffset),           // N
                new Vector2(0f, -yOffset),          // S
                new Vector2(xOffset, 0f),           // E
                new Vector2(-xOffset, 0f),          // W
                new Vector2(xOffset, yOffset),      // NE
                new Vector2(-xOffset, yOffset),     // NW
                new Vector2(xOffset, -yOffset),     // SE
                new Vector2(-xOffset, -yOffset),    // SW
            };

            foreach(var offset in offsets){
                Rect newRect = new Rect(rects[i].center + offset - thisRect.size/2f, thisRect.size);
                bool check = true;
                foreach(var rect2 in rects)
                    if(newRect.Overlaps(rect2)){
                        check = false;
                        break;
                    }
                if(
                    newRect.xMin < 0 ||
                    newRect.yMin < 0 ||
                    newRect.xMax > Screen.width ||
                    newRect.yMax > Screen.height
                ){
                    check = false;
                }
                if(check){
                    finalPos = newRect.center;
                    found = true;
                    break;
                }
            }

            if(found)
                break;
        }

        // finalPos.x = Mathf.Clamp(finalPos.x, 0, Screen.width - thisRect.width);
        // finalPos.y = Mathf.Clamp(finalPos.y, 0, Screen.height - thisRect.height);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            tooltipRect,
            finalPos,
            UICameraRef.uiCamera,
            out Vector3 worldPos
        );
        tooltipRect.position = worldPos;

        Complete();
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, duration).SetEase(ease);   
    }

    public void Deselect(){
        Complete();
        canvasGroup.DOFade(0f, duration).SetEase(ease)
            .onComplete = () => Destroy(gameObject);
    }

    private void Complete() => canvasGroup.DOComplete();

    private Rect GetRect(RectTransform rect){
        Canvas canvas = rect.GetComponentInParent<Canvas>();
        canvas.worldCamera = UICameraRef.uiCamera;

        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);

        Vector2 swCorner = UICameraRef.uiCamera.WorldToScreenPoint(corners[0]);
        Vector2 neCorner = UICameraRef.uiCamera.WorldToScreenPoint(corners[2]);
        return new Rect(swCorner, neCorner - swCorner);
    }
}
