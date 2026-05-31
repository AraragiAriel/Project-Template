using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class SmoothCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalText;
    [SerializeField] private TextMeshProUGUI diffText;

    [Header("Allot")]
    [SerializeField] private float delay;
    [SerializeField] private float allotTime;
    [SerializeField] private float maxStepTimer;

    [Header("Tween")]
    [SerializeField] private Ease ease = Ease.OutSine;
    [SerializeField] private float duration = .2f;
    [SerializeField] private Vector3 scale = new(-.1f, .4f, 0f);

    private Coroutine allotCo;
    private int target = 0;

    private int _currentAmount = 0;
    private int currentAmount
    {
        get => _currentAmount;
        set
        {
            _currentAmount = value;
            totalText.Set(_currentAmount.ToString());
        }
    }

    private int _variation = 0;
    private int variation
    {
        get => _variation;
        set
        {
            _variation = value;
            if(diffText != null)
                diffText.Set(
                    _variation == 0 ?
                        "" :
                        Util.ExposeSign(_variation)
                );
        }
    }

    private void Awake()
    {
        variation = 0;
        currentAmount = 0;
    }

    public void SetTarget(int target)
    {
        this.target = target;
        variation = target - currentAmount;
        Tween();

        if(allotCo != null)
            StopCoroutine(allotCo);
        allotCo = StartCoroutine(AllotCo());
    }

    private IEnumerator AllotCo()
    {
        yield return new WaitForSeconds(delay);

        float timeStep = Mathf.Min(allotTime/Mathf.Max(Mathf.Abs(variation) - 1, 1), maxStepTimer);
        float timer = 0f;
        while(true)
        {
            timer += Time.deltaTime;
            int steps = Mathf.Min(
                Mathf.FloorToInt(timer/timeStep),
                Mathf.Abs(variation)
            );
            Step(steps);
            timer -= steps*timeStep;
            if(variation == 0)
                break;

            yield return new WaitForEndOfFrame();
        }
        currentAmount = target;

        allotCo = null;
    }

    private void Step(int amount)
    {
        if(amount == 0)
            return;
            
        int step = Mathf.RoundToInt(Mathf.Sign(variation));
        variation -= step*amount;
        currentAmount += step*amount;
        Tween();
    }

    private void Tween()
    {
        foreach(TextMeshProUGUI tmp in 
            diffText == null ?
                new List<TextMeshProUGUI>(){totalText} :
                new List<TextMeshProUGUI>(){totalText, diffText}
        )
        {
            tmp.rectTransform.DOComplete();
            tmp.rectTransform.DOPunchScale(scale, duration).SetEase(ease);
        }
    }
}
