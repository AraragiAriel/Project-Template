using System;
using System.Collections;
using UnityEngine;

public class SortingSetter : MonoBehaviour
{
    public Action<float, int> OnSet; // height, order

    [SerializeField] private bool auto;
    [SerializeField] private bool onStart;
    [SerializeField] private float heightOffset;
    [SerializeField] private SortingSetter refSetter;

    private const int scale = 100;
    private Action<float> SetOrder;

    private SpriteRenderer sr;
    private Canvas canvas;
    private ParticleSystemRenderer ps;

    private Transform _t;
    private Transform t => _t ??= transform;

    private void Awake()
    {
        if(TryGetComponent(out sr))
        {
            SetOrder = SetOrderSr;
        }
        else if(TryGetComponent(out canvas))
        {
            SetOrder = SetOrderCanvas;
        }
        else if(TryGetComponent(out ps))
        {
            SetOrder = SetOrderParticleSystem;
        }
        else
        {
            enabled = false;
            return;
        }
    }

    void OnEnable()
    {
        if(refSetter != null)
            refSetter.OnSet += RefSetterLink;
            
        if(onStart)
            ContextSet();
        if(auto)
            StartCoroutine(SetCo());
    }

    void OnDisable()
    {
        if(refSetter != null)
            refSetter.OnSet -= RefSetterLink;        
    }

    private IEnumerator SetCo()
    {
        while(true)
        {
            ContextSet();
            yield return new WaitForEndOfFrame();
        }
    }

    public void ContextSet()
    {
        if(refSetter != null)
            Set(refSetter.transform.position.y);
        else
            Set(t.position.y);
    }

    public void RefSetterLink(float height, int order) => Set(height);
    public void Set(float height)
    {
        height += heightOffset;
        SetOrder(height);
        OnSet?.Invoke(height, Order(height));
    }

    private void SetOrderSr(float height)
    {
        sr.sortingOrder = Order(height);
    }

    private void SetOrderCanvas(float height)
    {
        canvas.sortingOrder = Order(height);
    }

    private void SetOrderParticleSystem(float height)
    {
        ps.sortingOrder = Order(height);
    }

    public void SetRef(SortingSetter setter)
    {
        if(refSetter != null)
            refSetter.OnSet -= RefSetterLink;

        refSetter = setter;
        refSetter.OnSet += RefSetterLink;
        ContextSet();
    }

    private int Order(float height) => Mathf.RoundToInt(-scale*height);
}
