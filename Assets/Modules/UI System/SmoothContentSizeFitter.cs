using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SmoothContentSizeFitter : MonoBehaviour
{
    private RectTransform _rt;
    private RectTransform rt => _rt ??= GetComponent<RectTransform>();

    [SerializeField] private bool horizontal = false;
    [SerializeField] private bool vertical   = false;

    [Header("Tween")]
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;

    private Vector2 lastSize = Vector2.zero;

    void Start()
    {
        lastSize = rt.sizeDelta;
        Check();
    }

    void OnChildRectTransformDimensionsChange()
    {
        Check();
    }

    private void Check()
    {
        Vector2 preferredSize = new(
            horizontal ? LayoutUtility.GetPreferredWidth(rt)  : rt.sizeDelta.x,
            vertical   ? LayoutUtility.GetPreferredHeight(rt) : rt.sizeDelta.y
        );
        if(Vector2.SqrMagnitude(preferredSize - lastSize) > .01f)
        {
            rt.DOKill(false);
            rt.DOSizeDelta(preferredSize, 1f).SetEase(ease);
            lastSize = preferredSize;
        }
    }
}
