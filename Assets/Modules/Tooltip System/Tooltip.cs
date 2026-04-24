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

    private TooltipTween _tween;
    private TooltipTween tween => _tween ??= GetComponent<TooltipTween>();
    
    protected TooltipData data;

    public void SetData(TooltipData data){
        // SETUP
        canvasGroup.alpha = 0f;
        this.data = data;
        if(TryGetComponent(out TooltipSetup setup))
        {
            setup.Setup(data);
        }
    }

    public void SetPosition()
    {
        var thisRect = tooltipRect.GetScreenRect();
        var rects = data.rects.Select(rect => rect.GetScreenRect()).ToList();
        Vector2 finalPos = Vector2.zero;
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

            foreach(var offset in offsets)
            {
                Rect newRect = new Rect(rects[i].center + offset - thisRect.size/2f, thisRect.size);
                bool check = true;
                foreach(var rect2 in rects)
                    if(newRect.Overlaps(rect2))
                    {
                        check = false;
                        break;
                    }
                if(
                    newRect.xMin < 0 ||
                    newRect.yMin < 0 ||
                    newRect.xMax > Screen.width ||
                    newRect.yMax > Screen.height
                )
                {
                    check = false;
                }
                if(check)
                {
                    finalPos = newRect.center;
                    found = true;
                    break;
                }
            }

            if(found)
                break;
        }

        tooltipRect.ToScreenPos(finalPos);
        tween.StartCoroutine(tween.Select(data.rects.Count - 1));
    }

    public void Deselect(){
        tween.StopAllCoroutines();
        tween.StartCoroutine(tween.Deselect());
    }
}