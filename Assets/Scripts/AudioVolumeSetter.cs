using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class AudioVolumeSetter : MonoBehaviour
{
    [SerializeField] private bool sfx = true;
    private AudioSource audioSource;
    private List<AudioMult> mults = new();

    private float settingsVolume => sfx ? AudioManager.sfxVolume : AudioManager.bgmVolume;
    
    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable(){
        StaticActions.OnGameSettingsChange += SetSettingsMult;
    }

    private void OnDisable(){
        StaticActions.OnGameSettingsChange -= SetSettingsMult;        
    }

    private void Start(){
        SetSettingsMult();
    }

    public void SetMult(AudioMultSource source, float value){
        bool found = false;
        foreach(AudioMult mult in mults)
            if(mult.source == source){
                mult.value = value;
                found = this;
                break;
            }
        if(!found)
            mults.Add(new AudioMult(source, value));

        SetVolume();
    }

    public void RemoveMult(AudioMultSource source){
        foreach(AudioMult mult in mults)
            if(mult.source == source){
                mults.Remove(mult);
                break;
            }

        SetVolume();
    }
    
    private void SetVolume(){
        float volume = 1f;
        foreach(AudioMult mult in mults)
            volume *= mult.source == AudioMultSource.Settings ? Mathf.Pow(mult.value, 2) : mult.value;
        audioSource.volume = volume;
    }

    private void SetSettingsMult(){
        SetMult(AudioMultSource.Settings, settingsVolume);
    }
}

public class AudioMult{
    public AudioMultSource source;
    public float value;

    public AudioMult(AudioMultSource source, float value){
        this.source = source;
        this.value = value;
    }
}

public enum AudioMultSource{
    Base,
    Fade,
    ComboLevel,
    Settings,
    ChallengeEnd,
    ComboUnlockScreen,
}
