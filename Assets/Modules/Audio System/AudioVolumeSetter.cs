using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeSetter : MonoBehaviour
{
    [SerializeField] private bool sfx = true;
    private AudioSource audioSource;
    private List<AudioMult> mults = new();
    private RID settingsID = new();

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

    public void SetMult(int id, float value){
        bool found = false;
        foreach(AudioMult mult in mults)
            if(mult.id == id){
                mult.value = value;
                found = this;
                break;
            }
        if(!found)
            mults.Add(new AudioMult(id, value));

        SetVolume();
    }

    public void RemoveMult(int id){
        foreach(AudioMult mult in mults)
            if(mult.id == id){
                mults.Remove(mult);
                break;
            }

        SetVolume();
    }
    
    private void SetVolume(){
        float volume = 1f;
        foreach(AudioMult mult in mults)
            audioSource.volume = volume;
    }

    private void SetSettingsMult(){
        SetMult(settingsID, settingsVolume);
    }
}

public class AudioMult{
    public int id;
    public float value;

    public AudioMult(int id, float value){
        this.id = id;
        this.value = value;
    }
}
