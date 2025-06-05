using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AfterEffectOnMove : MonoBehaviour
{
    [SerializeField] private float dist;
    [SerializeField] private float alpha;
    [SerializeField] private Sprite sprite;
    private SpriteRenderer sr;
    private Transform t;

    private void Awake(){
        t = transform;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start(){
        StartCoroutine(EmissionCo());
    }

    private IEnumerator EmissionCo(){
        float currentDist = 0f;
        Vector2 lastPos = t.position;

        while(true){
            currentDist += Vector2.Distance(t.position, lastPos);
            if(currentDist > dist){
                Emit();
                currentDist -= dist;
            }
            lastPos = t.position;
            yield return new WaitForEndOfFrame();
        }
    }

    private void Emit(){
        AfterEffect.Emit(new AfterEffectParameters(sr, sprite, alpha));
    }
}
