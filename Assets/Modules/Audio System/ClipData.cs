using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PitchType{
    None = 0,
    DefaultValue = 1,
    Custom = 2,
}

[CreateAssetMenu(fileName = "ClipData", menuName = "ScriptableObject/Clip Data")]
public class ClipData : ScriptableObject
{
    public const float defaultPitch = .1f;

    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(.1f, 2f)] public float basePitch = 1f;
    public PitchType pitchType = PitchType.None;
    public float customPitch = 0f;

    public float pitchRange{
        get{
            switch(pitchType){
                case PitchType.None:
                    return 0f;
                case PitchType.DefaultValue:
                    return defaultPitch;
                case PitchType.Custom:
                    return customPitch;
                default:
                    return 0f;
            }
        }
    }

    public float pitch{
        get{
            float range = pitchRange;
            return Random.Range(basePitch - range, basePitch + range);
        }
    }

    private void OnValidate(){
        basePitch = Util.Round(basePitch, .1f);
        volume = Util.Round(volume, .05f);
    }
}

[System.Serializable]
public class ClipsData{
    public List<ClipData> clips;
}