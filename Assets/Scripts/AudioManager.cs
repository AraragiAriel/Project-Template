using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // STATIC
    private const int initialAmount = 20;
    private static AudioManager instance;

    public static void PlayClip(ClipData clipData, float timer = 0f){
        if(!Check(clipData))
            return;

        instance.GetSource().Set2D(clipData, timer);
    }
      
    public static void PlayClip(ClipData clipData, Vector2 pos, float timer = 0f){
        if(!Check(clipData))
            return;

        instance.GetSource().Set3D(clipData, pos, timer);
    }

    #region UTILITIES

    private static bool Check(ClipData clipData){
        if(clipData == null)
            return false;
        if(clipData.clip == null)
            return false;

        return true;
    }

    public static float sfxVolume => ResourcesSystem.data.gameSettings.sfxVolume;
    public static float bgmVolume => ResourcesSystem.data.gameSettings.bgmVolume;

    #endregion

    // INSTANCE
    [SerializeField] private AudioSourceManager audioSourcePrefab;
    private List<AudioSourceManager> sources = new();

    private void Awake(){
        if(instance != null){
            Destroy(gameObject);
            return;
        } else {
            instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }

        for(int i = 0; i < initialAmount; i++)
            AddSource();
    }

    public AudioSourceManager GetSource(){
        foreach(AudioSourceManager source in sources)
            if(!source.isPlaying)
                return source;
        
        return AddSource();
    }

    public AudioSourceManager AddSource(){
        AudioSourceManager newSource = Instantiate(audioSourcePrefab, this.transform);
        sources.Add(newSource);
        return newSource;
    }
}
