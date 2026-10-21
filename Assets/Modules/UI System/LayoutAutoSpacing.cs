using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(HorizontalOrVerticalLayoutGroup), typeof(RectTransform))]
public class LayoutAutoSpacing : MonoBehaviour
{
    [SerializeField] private float minSpacing = -100;
    [SerializeField] private float maxSpacing = 100;

    [Header("Child Size")]
    [SerializeField] private bool useNativeSize = true;
    [SerializeField] private float overrideSize = 0f;

    private HorizontalOrVerticalLayoutGroup _layout;
    private HorizontalOrVerticalLayoutGroup layout => _layout??= GetComponent<HorizontalOrVerticalLayoutGroup>();

    private RectTransform _rect;
    private RectTransform rect => _rect??= GetComponent<RectTransform>();

    private void OnEnable() => Recalculate();
    private void OnTransformChildrenChanged() => Recalculate();
    private void OnRectTransformDimensionsChange() => Recalculate();

#if UNITY_EDITOR
    void OnValidate()
    {
        Recalculate();
    }
#endif

    void Recalculate()
    {
        int count = transform.childCount;
        if(count <= 1)
        {
            layout.spacing = 0;
            return;
        }

        bool horizontal = layout is HorizontalLayoutGroup;

        float available = horizontal ?
            rect.rect.width  - (layout.padding.left + layout.padding.right ) :
            rect.rect.height - (layout.padding.top  + layout.padding.bottom);

        float childTotalSize = 0f;
        if(useNativeSize)
            foreach(RectTransform child in rect)
                childTotalSize += horizontal ?
                    child.rect.width :
                    child.rect.height;
        else
            childTotalSize += count*overrideSize;

        available -= childTotalSize;
        float spacing = available/(count - 1);
        layout.spacing = Mathf.Clamp(spacing, minSpacing, maxSpacing);

        // LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
    }
}