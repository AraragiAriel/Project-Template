using System.Collections;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Expand Tween", menuName = "ScriptableObject/Tweens/Expand Tween")]
public class ExpandTween : TweenData<Transform>
{
    [Header("Pulse")]
    [SerializeField] private Ease ease = Ease.OutSine;
    [SerializeField] private float duration = .175f;
    [SerializeField] private Vector2 scale = new(1.2f, 1.2f);
    [SerializeField] private float str = 5;
    [SerializeField] private int vibrato = 20;
    [SerializeField] private int randomness = 90;

    protected override IEnumerator Perform(Parameters parameters)
    {
        var target = parameters.target as Transform;
        bool forward = parameters.type == Type.Forward;

        target.DOComplete();
        
        var seq = DOTween.Sequence();
        seq.SetTarget(target);
        seq.Join(
            target.DOScale(forward ? scale : Vector2.one, duration).SetEase(ease)
        );
        seq.Join(
            target.DOShakeRotation(duration, Vector3.forward*str, vibrato, randomness, false, randomnessMode: ShakeRandomnessMode.Harmonic).SetEase(ease)
        );
        yield return seq.WaitForCompletion();
    }

    public override void SetEnd(Parameters parameters)
    {
        var target = parameters.target as Transform;
        bool forward = parameters.type == Type.Forward;

        target.DOComplete();

        target.localScale = forward ? scale : Vector2.one;
    }
}