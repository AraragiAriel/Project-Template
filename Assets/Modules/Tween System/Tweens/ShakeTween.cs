using System.Collections;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Shake Tween", menuName = "ScriptableObject/Tweens/Shake Tween")]
public class ShakeTween : TweenData<Transform>
{
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;
    [SerializeField] private float positionStr;
    [SerializeField] private Vector3 rotationStr;
    [SerializeField] private int vibrato;

    protected override IEnumerator Perform(Parameters parameters)
    {
        var target = parameters.target as Transform;
        target.DOKill(true);
        var seq = DOTween.Sequence();
        seq.SetTarget(target);

        seq.Join(
            target.DOShakePosition(duration, positionStr, vibrato).SetEase(ease).SetUpdate(UpdateType.Fixed)
        );

        seq.Join(
            target.DOShakeRotation(duration, rotationStr, vibrato).SetEase(ease).SetUpdate(UpdateType.Fixed)
        );

        yield return seq.WaitForCompletion();
    }
}
