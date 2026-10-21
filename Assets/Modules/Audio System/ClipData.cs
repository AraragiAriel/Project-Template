using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClipData", menuName = "ScriptableObject/Audio/Clip Data")]
public class ClipData : ScriptableObject
{
    public const float defaultPitch = .1f;
    public const float defaultDelay = .1f;

    [Space()]
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;

    [Header("Pitch")]
    public float basePitch = 1f;
    public PitchType pitchType = PitchType.None;
    [Tooltip("Random value: [basePitch - customPitch, basePitch + customPitch]")]
    public float customPitch = 0f;

    [Header("Delay")]
    public DelayType delayType = DelayType.None;
    [Range(0f, .5f)] public float delayFrom = 0f;
    [Range(0f, .5f)] public float delayTo = 0f;

    public float pitch
    {
        get
        {
            return pitchType switch
            {
                PitchType.None => basePitch,
                PitchType.DefaultValue => basePitch + Random.Range(-defaultPitch, defaultPitch),
                PitchType.Custom => basePitch + Random.Range(-customPitch, customPitch),
                _ => basePitch,
            };
        }
    }

    public float delay
    {
        get
        {
            return delayType switch
            {
                DelayType.None => 0f,
                DelayType.DefaultValue => Random.Range(0f, defaultDelay),
                DelayType.To => Random.Range(0f, delayTo),
                DelayType.FromTo => Random.Range(delayFrom, delayTo),
                _ => 0f,
            };
        }
    }
}

[System.Serializable]
public class ClipsData
{
    public List<ClipData> clips;
}

public enum PitchType
{
    None = 0,
    DefaultValue = 1,
    Custom = 2,
}

public enum DelayType
{
    None = 0,
    DefaultValue = 1,
    To = 2,
    FromTo = 3,
}