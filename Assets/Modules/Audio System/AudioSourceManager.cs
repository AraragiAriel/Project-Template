using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioVolumeSetter volumeSetter;
    [Range(0f, 1f)]
    [SerializeField] private float spatialBlend;

    private RID id;

    public bool isPlaying{get{return audioSource.isPlaying;}}

    private void OnEnable(){
        StaticActions.OnSceneChange += OnSceneChange;
    }

    private void OnDisable(){
        StaticActions.OnSceneChange -= OnSceneChange;        
    }

    public void Play(AudioManager.Parameters parameters){
        if(isPlaying)
            Debug.LogWarning("overwriting active audio source");

        if(parameters.usePos){
            transform.position = parameters.pos;
            audioSource.spatialBlend = spatialBlend;
        } else {
            audioSource.spatialBlend = 0f;
        }
        audioSource.clip = parameters.data.clip;
        volumeSetter.SetMult(id, parameters.data.volume);
        audioSource.pitch = parameters.data.pitch;
        audioSource.time = parameters.data.clip.length*parameters.timer;
        audioSource.Play();
    }

    private void OnSceneChange(SceneType scene){
        audioSource.Stop();
    }
}
