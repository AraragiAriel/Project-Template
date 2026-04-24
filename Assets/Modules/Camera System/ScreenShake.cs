using UnityEngine;
using DG.Tweening;

public class ScreenShake : MonoBehaviour
{
    public class Parameters{
        public float mult = 1f;
    }

    public static ScreenShake instance;

    public static void Shake(){
        instance.InstanceShake(new Parameters());
    }

    public static void Shake(Parameters param){
        instance.InstanceShake(param);
    }

    //INSTANCE

    [SerializeField] private float str;
    [SerializeField] private float duration;
    [SerializeField] private int vibrato;
    [SerializeField] private Ease ease;
    private Transform t;

    private void Awake(){
        instance = this;
        t = transform;
    }

    private void InstanceShake(Parameters param){
        t.DOComplete();
        t.DOShakePosition(duration, str*param.mult, vibrato, randomnessMode: ShakeRandomnessMode.Harmonic).SetEase(ease);
        // t.DOShakeRotation(duration, str, randomnessMode: ShakeRandomnessMode.Harmonic).SetEase(ease);
    }
}
