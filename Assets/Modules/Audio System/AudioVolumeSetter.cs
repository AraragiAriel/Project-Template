using UnityEngine;

public class AudioVolumeSetter : MonoBehaviour
{
    [SerializeField] private bool sfx = true;

    private AudioSource audioSource;
    private RID settingsID = new();
    private CompositeValue volume = new(){
        baseValue = 1f,
    };

    private float settingsVolume => sfx ? AudioManager.sfxVolume : AudioManager.bgmVolume;
    
    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable(){
        StaticActions.OnGameSettingsChange += ApplySettings;
        volume.OnValueChange += SetVolume;
    }

    private void OnDisable(){
        StaticActions.OnGameSettingsChange -= ApplySettings;    
        volume.OnValueChange -= SetVolume;    
    }

    private void Start(){
        ApplySettings();
    }

    public void SetMult(RID id, float value) => volume.SetMod(new ValueMod(id, value, ValueMod.Type.Mult));
    public void RemoveMult(RID id) => volume.RemoveMod(id);    
    private void SetVolume(float value) => audioSource.volume = value;

    private void ApplySettings() => SetMult(settingsID, settingsVolume);
}
