using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Fade Tween", menuName = "ScriptableObject/Tweens/Fade Tween")]
public class FadeTween : TweenData<ColorApply>
{
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;
    [SerializeField] private Vector2 alpha = new(0f, 1f);

    protected override IEnumerator Perform(Parameters parameters)
    {
        var target = parameters.target as ColorApply;

        target.DOKill();
        float from;
        float to;
        if(parameters.type == Type.Forward)
        {
            from = alpha.x;
            to = alpha.y;
        }
        else
        {
            from = alpha.y;
            to = alpha.x;
        }
        yield return
        DOTween.To(
            () => from,
            x =>
            {
                from = x;
                target.Set(x);
            },
            to,
            duration
        ).SetEase(ease).WaitForCompletion();
    }

    protected override void SetEndInternal(Parameters parameters)
    {
        var target = parameters.target as ColorApply;

        target.DOKill();
        target.Set(parameters.type == Type.Forward ? alpha.y : alpha.x);
    }
}
