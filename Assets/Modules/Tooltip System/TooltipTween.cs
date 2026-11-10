using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class TooltipTween : MonoBehaviour
{
    private const float delay = .1f;

    private RectTransform _tooltipRect;
    protected RectTransform rect => _tooltipRect ??= transform.GetChild(0).GetComponent<RectTransform>();
    private int order;

    private TweenUse _tween;
    private TweenUse tween => _tween ??= GetComponent<TweenUse>();

    public IEnumerator Select(int order)
    {
        if(tween == null) yield break;

        this.order = order;
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(order*delay);
        yield return StartCoroutine(tween.Tween(TweenData.Type.Forward));
    }

    public IEnumerator Deselect()
    {
        if(tween == null) yield break;

        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(order*delay);
        yield return StartCoroutine(tween.Tween(TweenData.Type.Reverse));
    }
}
