using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorApply : MonoBehaviour
{
    private SpriteRenderer sr;
    private TextMeshProUGUI tmp;
    private Image image;
    private CanvasGroup canvasGroup;

    private Action<Color> SetColor;
    private Action<float> SetAlpha;

    void Awake()
    {
        if(TryGetComponent(out sr))
        {
            SetColor = SetSrColor;
            SetAlpha = SetSrAlpha;
        }
        else if(TryGetComponent(out tmp))
        {
            SetColor = SetTmpColor;
            SetAlpha = SetTmpAlpha;
        }
        else if(TryGetComponent(out image))
        {
            SetColor = SetImageColor;
            SetAlpha = SetImageAlpha;
        }
        else if(TryGetComponent(out canvasGroup))
        {
            SetColor = SetCanvasGroupColor;
            SetAlpha = SetCanvasGroupAlpha;
        }
        else
        {
            Debug.LogWarning("Missing color setter target");
        }
    }

    public void Set(Color color) => SetColor?.Invoke(color);
    public void Set(float alpha) => SetAlpha?.Invoke(alpha);

    private void SetSrColor(Color color) => sr.color = color;
    private void SetSrAlpha(float alpha) => sr.SetAlpha(alpha);

    private void SetTmpColor(Color color) => tmp.color = color;
    private void SetTmpAlpha(float alpha) => tmp.alpha = alpha;

    private void SetImageColor(Color color) => image.color = color;
    private void SetImageAlpha(float alpha) => image.SetAlpha(alpha);

    private void SetCanvasGroupColor(Color color) => canvasGroup.alpha = color.a;
    private void SetCanvasGroupAlpha(float alpha) => canvasGroup.alpha = alpha;
}
