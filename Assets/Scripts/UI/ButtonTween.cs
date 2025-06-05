using UnityEngine;
using DG.Tweening;

public class ButtonTween : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
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
        events.onSelect.AddListener(OnSelect);
        events.onDeselect.AddListener(OnDeselect);
    }

    private void OnDisable(){
        events.onSelect.RemoveListener(OnSelect);
        events.onDeselect.RemoveListener(OnDeselect);
    }

    private void Start(){
        initialRot = rect.eulerAngles;
    }

    public void OnSelect(){
        Tween(true);
    }

    public void OnDeselect(){
        Tween(false);
    }

    public void Tween(bool select){
        rect.DOKill(true);
        rect.DOScale(select ? scale : 1f, duration).SetEase(ease);
        initialRot = rect.eulerAngles;
        rect.DOShakeRotation(duration, new Vector3(0f, 0f, shakeStr), randomnessMode: ShakeRandomnessMode.Harmonic).SetEase(ease).onComplete = () => 
            rect.DOLocalRotate(initialRot, duration*.25f);

    }
}
