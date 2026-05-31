using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class DevToolsUI : MonoBehaviour
{
    [SerializeField] private RectTransform layout;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] DevToolLine prefab;

    [Header("Tween")]
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;
    [SerializeField] private float pos;

    private bool open = false;

    public void Setup(List<DevTools.Slot> slots)
    {
        Util.DestroyAllChildren(layout);

        foreach(var slot in slots)
        {
            var line = Instantiate(prefab, layout);
            line.Setup(slot);
        }

        open = false;
        layout.anchoredPosition = new Vector2(pos, layout.anchoredPosition.y);
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
    }

    public void Toggle()
    {
        open = !open;

        canvasGroup.interactable = open;

        layout.DOKill();
        canvasGroup.DOKill();

        canvasGroup.DOFade(open ? 1f : 0f, duration).SetEase(ease);
        layout.DOAnchorPosX(open ? 0f : pos, duration).SetEase(ease);
    }
}
