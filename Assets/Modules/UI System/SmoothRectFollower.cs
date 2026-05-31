using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SmoothRectFollower : MonoBehaviour
{
    [SerializeField] private RectTransform target;

    private RectTransform _t;
    private RectTransform t => _t ??= GetComponent<RectTransform>();

    private Vector2 targetLastPos;
    private bool setup = false;

    // TWEEN
    private const Ease ease = Ease.OutSine;
    private const float duration = 1f;
    private Tween tween;

    private void Awake(){
        if(target == null)
            target = t.parent.GetComponent<RectTransform>();
        StartCoroutine(SetupCo());
    }

    void OnEnable()
    {
        Canvas.willRenderCanvases += Tick;
    }

    void OnDisable()
    {
        Canvas.willRenderCanvases -= Tick;
    }

    private void Tick()
    {
        if(!setup)
            return;

        if(target == null){
            Destroy(gameObject);
            return;
        }

        Vector2 diff = target.anchoredPosition - targetLastPos;
        if(Vector2.SqrMagnitude(diff) > 0.01f){
            t.anchoredPosition -= diff;
            if(tween != null)
            {
                tween.Kill();
                tween = null;
            }
            tween = t.DOAnchorPos(Vector2.zero, duration).SetEase(ease);
        }

        targetLastPos = target.anchoredPosition;
    }

    private IEnumerator SetupCo(){
        var canvasGroup = gameObject.AddComponent<CanvasGroup>();
        // canvasGroup.alpha = 0f;

        yield return new WaitForEndOfFrame();

        targetLastPos = target.anchoredPosition;
        t.anchoredPosition = Vector2.zero;

        Destroy(canvasGroup);
        setup = true;
    }
}
