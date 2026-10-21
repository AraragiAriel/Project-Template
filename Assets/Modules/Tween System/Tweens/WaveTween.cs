using System.Collections;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Tween", menuName = "ScriptableObject/Tweens/Wave Tween")]
public class WaveTween : TweenData<Transform>
{
    [Header("Wave")]
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;
    [SerializeField] private Vector2 distance;
    [SerializeField] private Vector2 sparse;

    protected override IEnumerator Perform(Parameters parameters)
    {
        var target = parameters.target as Transform;

        target.DOComplete();
        Vector2 initialPos = target.localPosition;
        
        var seq = DOTween.Sequence();
        seq.SetTarget(target);

        var tween = 
            target.DOLocalMove(initialPos + distance, duration)
            .From(initialPos - distance)
            .SetEase(ease)
        ;

        seq.Join(tween);
        seq.SetLoops(-1, LoopType.Yoyo);
        float dot = Vector2.Dot(target.localPosition, sparse)*duration;
        seq.Goto(Mathf.Repeat(dot, duration*2f), true);

        yield return seq.WaitForCompletion();
    }
}
