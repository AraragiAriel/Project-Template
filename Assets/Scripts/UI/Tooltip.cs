using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private const float duration = .25f;
    private const Ease ease = Ease.OutSine;

    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake() {
        if(canvasGroup == null)
            enabled = false;
    }

    private void OnEnable(){
        if(TryGetComponent(out ButtonEvents events)){
            events.onSelect.AddListener(Select);
            events.onDeselect.AddListener(Deselect);
        }
    }

    private void OnDisable(){
        if(TryGetComponent(out ButtonEvents events)){
            events.onSelect.RemoveListener(Select);
            events.onDeselect.RemoveListener(Deselect);
        }       
    }

    private void Start(){
        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }

    public void Select(){
        Complete();
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.DOFade(1f, duration).SetEase(ease);
    }

    public void Deselect(){
        Complete();
        canvasGroup.DOFade(0f, duration).SetEase(ease)
            .onComplete = () => canvasGroup.gameObject.SetActive(false);
    }

    private void Complete(){
        canvasGroup.DOComplete();
    }
}
