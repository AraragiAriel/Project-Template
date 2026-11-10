using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    // STATIC
    public static void Play(ClipData data) => Play(new Parameters(data));
    public static void Play(Parameters parameters)
    {
        if(!Check(parameters.data))
            return;

        instance.PlayClip(parameters);
    }

    private static bool Check(ClipData clipData)
    {
        if(clipData == null)
            return false;
        if(clipData.clip == null)
            return false;

        return true;
    }

    public static float sfxVolume => Res.data.gameSettingsData.sfxVolume;
    public static float bgmVolume => Res.data.gameSettingsData.bgmVolume;

    // INSTANCE
    private const int initialAmount = 20;

    [SerializeField] private AudioSourceManager audioSourcePrefab;
    private List<AudioSourceManager> sources = new();

    protected override void Setup()
    {
        base.Setup();

        for(int i = 0; i < initialAmount; i++)
            AddSource();
    }

    private void PlayClip(Parameters parameters)
    {
        StartCoroutine(PlayCo(parameters));
    }

    private IEnumerator PlayCo(Parameters parameters)
    {
        yield return new WaitForSeconds(parameters.data.delay);
        GetSource().Play(parameters);        
    }

    public AudioSourceManager GetSource()
    {
        foreach(AudioSourceManager source in sources)
            if(!source.isPlaying)
                return source;
        
        return AddSource();
    }

    public AudioSourceManager AddSource()
    {
        AudioSourceManager newSource = Instantiate(audioSourcePrefab, this.transform);
        sources.Add(newSource);
        return newSource;
    }

    public class Parameters
    {
        public ClipData data;
        public float timer = 0f;
        public bool usePos = false;
        public Vector2 pos = default;

        public Parameters(ClipData data)
        {
            this.data = data;
        }
    }
}
