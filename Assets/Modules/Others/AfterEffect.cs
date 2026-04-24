using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AfterEffect : MonoBehaviour
{
    public class Parameters{
        private const float defaultDuration = .6f;
        private const float defaultAlpha = .02f;

        public SpriteRenderer sr;
        public Sprite sprite;
        public Transform t;
        public float duration;
        public float alpha;

        public Parameters(SpriteRenderer sr, float alpha = defaultAlpha){
            this.t = sr.transform;
            this.duration = defaultDuration;
            this.sr = sr;
            this.alpha = alpha;
        }

        public Parameters(SpriteRenderer sr, Sprite sprite, float alpha = defaultAlpha){
            this.t = sr.transform;
            this.duration = defaultDuration;
            this.sr = sr;
            this.alpha = alpha;
            this.sprite = sprite;
        }
    }


    public static void Emit(Parameters parameters){
        AfterEffect afterEffect = Instantiate(Res.data.afterEffect, parameters.t.position, Quaternion.identity);
        afterEffect.Set(parameters);
    }
    
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Ease ease;

    public void Set(Parameters parameters){
        transform.rotation = parameters.t.rotation;
        transform.localScale = parameters.t.localScale;
        sr.sprite = parameters.sprite == null ? parameters.sr.sprite : parameters.sprite;
        var aux = parameters.sr.color;
        aux.a = parameters.alpha;
        sr.color = aux;
        sr.DOFade(0f, parameters.duration).SetEase(ease).onComplete = () => Destroy(gameObject);
    }
}
