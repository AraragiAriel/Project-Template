using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class StatUI : MonoBehaviour, ITooltip
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;

    [Header("Tween")]
    [SerializeField] private Transform t;
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;
    [SerializeField] private Vector2 increaseScale;
    [SerializeField] private Vector2 decreaseScale;
    [SerializeField] private float str;

    [HideInInspector] public Stat stat;

    private float lastValue;

    public TooltipData TooltipData() => new StatTooltipData
    {
        prefab = Res.data.statTooltip,
        stat = stat,
    };

    public void Set(Stat stat)
    {
        this.stat = stat;
        stat.OnValueChange += ValueChange;

        // INITIAL UI SET
        icon.sprite = stat.icon;
        SetUI();
    }

    private void ValueChange(float value) => SetUI();
    private void SetUI()
    {
        text.Set(stat.ValueText());
        
        if(!Mathf.Approximately(stat, lastValue))
        {
            t.DOComplete();
            if(stat > lastValue)
            {
                // INCREASE
                t.DOPunchScale(increaseScale, duration).SetEase(ease);
                t.DOShakeRotation(duration, Vector3.forward*str).SetEase(ease);
            }
            else
            {
                // DECREASE
                t.DOPunchScale(decreaseScale, duration).SetEase(ease);
                t.DOShakeRotation(duration, Vector3.forward*str).SetEase(ease);
            }
        }
        lastValue = stat;
    }

    private void OnDestroy()
    {
        if(stat != null)
            stat.OnValueChange -= ValueChange;
    }
}
