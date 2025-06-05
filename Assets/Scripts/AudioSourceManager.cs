using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioVolumeSetter volumeSetter;
    [Range(0f, 1f)]
    [SerializeField] private float spatialBlend;    

    public bool isPlaying{get{return audioSource.isPlaying;}}

    private void OnEnable(){
        StaticActions.OnSceneChange += OnSceneChange;
    }

    private void OnDisable(){
        StaticActions.OnSceneChange -= OnSceneChange;        
    }
    
    public void Set3D(ClipData data, Vector2 pos, float timer){
        transform.position = pos;
        audioSource.spatialBlend = spatialBlend;
        Set(data, timer);
    }
    
    public void Set2D(ClipData data, float timer){
        audioSource.spatialBlend = 0f;
        Set(data, timer);
    }

    private void Set(ClipData data, float timer){
        if(isPlaying)
            Debug.LogWarning("overwriting active audio source");

        audioSource.clip = data.clip;
        volumeSetter.SetMult(AudioMultSource.Base, data.volume);
        audioSource.pitch = data.pitch;
        audioSource.time = data.clip.length*timer;
        audioSource.Play();
    }

    private void OnSceneChange(SceneType scene){
        audioSource.Stop();
    }
}
