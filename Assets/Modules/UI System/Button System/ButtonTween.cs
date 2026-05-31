using UnityEngine;
using DG.Tweening;

public class ButtonTween : MonoBehaviour
{
    [SerializeField] private Transform t;

    [Header("Tween")]
    [SerializeField] private float duration = .25f;
    [SerializeField] private float height;
    [SerializeField] private float scale;
    [SerializeField] private float shakeStr;
    [SerializeField] private Ease ease;
    private Vector3 initialRot;
    private ButtonEvents events;

    private void Awake(){
        events = GetComponent<ButtonEvents>();
    }

    private void OnEnable(){
        events.OnSelect.AddListener(OnSelect);
        events.OnDeselect.AddListener(OnDeselect);
    }

    private void OnDisable(){
        events.OnSelect.RemoveListener(OnSelect);
        events.OnDeselect.RemoveListener(OnDeselect);
    }

    private void Start(){
        initialRot = t.localEulerAngles;
    }

    public void OnSelect(){
        Tween(true);
    }

    public void OnDeselect(){
        Tween(false);
    }

    public void Tween(bool select){
        t.DOKill(true);
        t.DOScale(select ? scale : 1f, duration).SetEase(ease);
        t.DOShakeRotation(duration, new Vector3(0f, 0f, shakeStr), randomnessMode: ShakeRandomnessMode.Harmonic).SetEase(ease).onComplete = () => 
            t.DOLocalRotate(initialRot, duration*.25f);
    }
}
