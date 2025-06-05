using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BgmSourceManager : MonoBehaviour
{
    [SerializeField] private ClipData data;
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDurationMult;
    private AudioVolumeSetter volumeSetter;
    private AudioSource audioSource;
    private float currentFade = 0f;

    private void Awake(){
        volumeSetter = GetComponent<AudioVolumeSetter>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable(){
        StaticActions.OnSceneBeginChange += SceneBeginChange;
    }

    private void OnDisable(){
        StaticActions.OnSceneBeginChange -= SceneBeginChange;        
    }

    private void Start(){
        if(data == null)
            return;

        volumeSetter.SetMult(AudioMultSource.Base, data.volume);
        audioSource.clip = data.clip;
        audioSource.pitch = data.pitch;
        audioSource.Play();

        StopAllCoroutines();
        StartCoroutine(FadeInCo());
    }

    private void SceneBeginChange(SceneType scene, float duration){
        StopAllCoroutines();
        StartCoroutine(FadeOutCo(duration));
    }

    private IEnumerator FadeInCo(){
        volumeSetter.SetMult(AudioMultSource.Fade, 0f);
        float timer = 0f;
        while(timer < fadeInDuration){
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            currentFade = timer/fadeInDuration;
            volumeSetter.SetMult(AudioMultSource.Fade, currentFade);
        }
        volumeSetter.RemoveMult(AudioMultSource.Fade);
    }

    private IEnumerator FadeOutCo(float sceneDuration){
        float timer = 0f;
        float totalDuration = sceneDuration*fadeOutDurationMult;
        while(timer < totalDuration){
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            volumeSetter.SetMult(AudioMultSource.Fade, currentFade*(1f - timer/totalDuration));
        }
    }
}
