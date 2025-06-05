using UnityEngine;
using DG.Tweening;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;

    public static void Shake(){
        instance.InstanceShake();
    }

    //INSTANCE

    [SerializeField] private Vector3 str;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;
    private Transform t;

    private void Awake(){
        instance = this;
        t = transform;
    }

    private void OnEnable(){
    }

    private void OnDisable(){        
    }

    private void InstanceShake(){
        t.DOComplete();
        t.DOShakeRotation(duration, str, randomnessMode: ShakeRandomnessMode.Harmonic).SetEase(ease);
    }

    private void Hurt(bool expire){
        InstanceShake();
    }
}
