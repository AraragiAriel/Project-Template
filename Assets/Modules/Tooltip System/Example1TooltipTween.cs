using DG.Tweening;
using UnityEngine;

public class RogamolTooltipTween : TooltipTween
{
    [SerializeField] private Ease inEase;
    [SerializeField] private Ease outEase;
    [SerializeField] private float duration;
    [SerializeField] private float fromScale;
    [SerializeField] private float fromRotation;

    protected override void TweenIn()
    {
        rect.DOScale(Vector3.one, duration)
            .From(Vector3.one*fromScale)
            .SetEase(inEase);
        rect.DOLocalRotate(Vector3.zero, duration)
            .From(Vector3.forward*fromRotation)
            .SetEase(inEase);
        canvasGroup.DOFade(1f, duration).SetEase(inEase);
    }

    protected override void TweenOut()
    {
        canvasGroup.DOKill();
        rect.DOKill();
        rect.DOScale(Vector3.one*fromScale, duration).SetEase(outEase);
        rect.DOLocalRotate(-Vector3.forward*fromRotation, duration).SetEase(outEase);
        canvasGroup.DOFade(0f, duration).SetEase(outEase)
            .onComplete = () => Destroy(gameObject);
    }
}