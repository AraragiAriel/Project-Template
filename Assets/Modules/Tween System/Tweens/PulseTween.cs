using System.Collections;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Pulse Tween", menuName = "ScriptableObject/Tweens/Pulse Tween")]
public class PulseTween : TweenData<Transform>
{
    [Header("Pulse")]
    [SerializeField] private Ease ease = Ease.OutSine;
    [SerializeField] private float duration = .175f;
    [SerializeField] private Vector2 scale = new(.2f, .2f);
    [SerializeField] private float str = 5;
    [SerializeField] private int vibrato = 20;
    [SerializeField] private int randomness = 90;

    protected override IEnumerator Perform(Parameters parameters)
    {
        var target = parameters.target as Transform;

        target.DOComplete();
        
        var seq = DOTween.Sequence();
        seq.SetTarget(target);
        seq.Join(
            target.DOPunchScale(scale, duration).SetEase(ease)
        );
        seq.Join(
            target.DOShakeRotation(duration, Vector3.forward*str, vibrato, randomness, false, randomnessMode: ShakeRandomnessMode.Harmonic).SetEase(ease)
        );
        yield return seq.WaitForCompletion();
    }
}
