using System.Collections;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Slide Tween", menuName = "ScriptableObject/Tweens/Slide Tween")]
public class SlideTween : TweenData<Transform>
{
    [Header("Slide")]
    [SerializeField] private Ease slideEase;
    [SerializeField] private float slideDuration;
    [SerializeField] private Vector2 initialPos;
    [SerializeField] private Vector2 finalPos;

    [Header("Scale")]
    [SerializeField] private Ease scaleEase;
    [SerializeField] private float scaleDuration;
    [SerializeField] private Vector2 thinScale;
    [SerializeField] private Vector2 wideScale;

    protected override IEnumerator Perform(Parameters parameters)
    {
        var target = parameters.target as Transform;

        target.DOComplete();

        Vector2 from;
        Vector2 to;
        if(parameters.type == Type.Forward)
        {
            from = initialPos;
            to = finalPos;
        }
        else
        {
            from = finalPos;
            to = initialPos;
        }

        target.localPosition = from;

        var seq = DOTween.Sequence();
        seq.SetTarget(target);

        seq.Join(target.DOScale(thinScale, scaleDuration).SetEase(scaleEase));
        seq.Join(target.DOLocalMove(to, slideDuration).SetEase(slideEase));
        seq.Insert(slideDuration - scaleDuration, target.DOScale(wideScale, scaleDuration).SetEase(scaleEase));
        seq.Append(target.DOScale(Vector2.one, scaleDuration).SetEase(scaleEase));

        yield return seq.WaitForCompletion();
    }

    protected override void SetEndInternal(Parameters parameters)
    {
        var target = parameters.target as Transform;

        target.DOKill();
        target.localPosition = parameters.type == Type.Forward ? finalPos : initialPos;
    }
}
