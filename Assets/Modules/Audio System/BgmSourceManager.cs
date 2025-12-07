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
    private RID baseID = new();
    private RID fadeID = new();

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

        volumeSetter.SetMult(baseID, data.volume);
        audioSource.clip = data.clip;
        audioSource.pitch = data.pitch;
        audioSource.Play();

        StopAllCoroutines();
        StartCoroutine(FadeInCo());
    }

    private void SceneBeginChange(SceneType scene){
        StopAllCoroutines();
        StartCoroutine(FadeOutCo(1f));
    }

    private IEnumerator FadeInCo(){
        volumeSetter.SetMult(fadeID, 0f);
        float timer = 0f;
        while(timer < fadeInDuration){
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            currentFade = timer/fadeInDuration;
            volumeSetter.SetMult(fadeID, currentFade);
        }
        volumeSetter.RemoveMult(fadeID);
    }

    private IEnumerator FadeOutCo(float sceneDuration){
        float timer = 0f;
        float totalDuration = sceneDuration*fadeOutDurationMult;
        while(timer < totalDuration){
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            volumeSetter.SetMult(fadeID, currentFade*(1f - timer/totalDuration));
        }
    }
}
