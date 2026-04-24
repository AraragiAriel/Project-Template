using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PitchType{
    None = 0,
    DefaultValue = 1,
    Custom = 2,
}

public enum DelayType{
    None = 0,
    DefaultValue = 1,
    To = 2,
    FromTo = 3,
}

[CreateAssetMenu(fileName = "ClipData", menuName = "ScriptableObject/Clip Data")]
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

#if UNITY_EDITOR
    [Header("Check")]
    public bool implementado = false;
#endif

    public float pitch{
        get{
            switch(pitchType){
                case PitchType.None:
                    return basePitch;
                case PitchType.DefaultValue:
                    return basePitch + Random.Range(-defaultPitch, defaultPitch);
                case PitchType.Custom:
                    return basePitch + Random.Range(-customPitch, customPitch);
                default:
                    return basePitch;
            }
        }
    }

    public float delay{
        get{
            switch(delayType){
                case DelayType.None:
                    return 0f;
                case DelayType.DefaultValue:
                    return Random.Range(0f, defaultDelay);
                case DelayType.To:
                    return Random.Range(0f, delayTo);
                case DelayType.FromTo:
                    return Random.Range(delayFrom, delayTo);
                default:
                    return 0f;
            }
        }
    }

    private void OnValidate(){
        basePitch = Util.Round(basePitch, .05f);
        volume = Util.Round(volume, .05f);
        delayFrom = Util.Round(delayFrom, .01f);
        delayTo = Util.Round(delayTo, .01f);
    }
}

[System.Serializable]
public class ClipsData{
    public List<ClipData> clips;
}