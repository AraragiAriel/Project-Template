using System.Collections;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Spin Tween", menuName = "ScriptableObject/Tweens/Spin Tween")]
public class SpinTween : TweenData<Transform>
{
    [Header("Spin")]
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;
    [SerializeField] private bool clockwise = true;

    protected override IEnumerator Perform(Parameters parameters)
    {
        var target = parameters.target as Transform;

        target.DOKill(true);
        
        target.DOLocalRotate(360f*Vector3.forward*(!clockwise).ToInt(), duration, RotateMode.FastBeyond360).SetEase(ease).SetLoops(-1, LoopType.Incremental);

        yield break;
    }
}
