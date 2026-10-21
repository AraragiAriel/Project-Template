using UnityEngine;

[CreateAssetMenu(fileName = "Audio Volume Setting", menuName = "ScriptableObject/Game Settings/Audio Volume")]
public class AudioVolumeSetting : GameSetting
{
    private const string masterVolume = "masterVolume";
    private const string bgmVolume    = "bgmVolume";
    private const string sfxVolume    = "sfxVolume";

    public override void Load()
    {
        data.masterVolume = PlayerPrefs.GetFloat(masterVolume, 1f);
        data.bgmVolume    = PlayerPrefs.GetFloat(bgmVolume, 1f);
        data.sfxVolume    = PlayerPrefs.GetFloat(sfxVolume, 1f);
    }

    public override void Apply()
    {
    }

    public override void Save()
    {
        PlayerPrefs.SetFloat(masterVolume, data.masterVolume);
        PlayerPrefs.SetFloat(bgmVolume   , data.bgmVolume);
        PlayerPrefs.SetFloat(sfxVolume   , data.sfxVolume);
    }
}
