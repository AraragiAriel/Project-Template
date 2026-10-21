using System.Collections;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Scale Tween", menuName = "ScriptableObject/Tweens/Scale Tween")]
public class ScaleTween : TweenData<Transform>
{
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;
    [SerializeField] private float fromScale;
    [SerializeField] private float toScale;
    [SerializeField] private float fromRotation;

    protected override IEnumerator Perform(Parameters parameters)
    {
        var target = parameters.target as Transform;
        target.DOKill();
        var seq = DOTween.Sequence();
        seq.SetTarget(target);
        bool forward = parameters.type == Type.Forward;

        seq.Join(
            target.DOScale(forward ? toScale : fromScale, duration)
                .From(Vector3.one*(forward ? fromScale : toScale))
                .SetEase(ease)
        );
        seq.Join(
            target.DOLocalRotate(forward ? Vector3.zero : -Vector3.forward*fromRotation, duration)
                .From(forward ? Vector3.forward*fromRotation : Vector3.zero)
                .SetEase(ease)
        );

        yield return seq.WaitForCompletion();
    }

    protected override void SetEndInternal(Parameters parameters)
    {
        var target = parameters.target as Transform;
        bool forward = parameters.type == Type.Forward;

        target.DOKill();
        target.localScale = Vector3.one*(forward ? toScale : fromScale);
        target.localEulerAngles = Vector3.forward*(forward ? 0f : fromRotation);
    }
}
