using UnityEngine;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public abstract class TooltipTween : MonoBehaviour
{
    private const float delay = .1f;

    private RectTransform _tooltipRect;
    protected RectTransform rect => _tooltipRect ??= transform.GetChild(0).GetComponent<RectTransform>();
    private int order;

    private CanvasGroup _canvasGroup;
    protected CanvasGroup canvasGroup => _canvasGroup ??= GetComponent<CanvasGroup>();

    public IEnumerator Select(int order)
    {
        this.order = order;
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(order*delay);
        TweenIn();
    }

    public IEnumerator Deselect()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(order*delay);
        TweenOut();
    }

    protected abstract void TweenIn();
    protected abstract void TweenOut();
}
