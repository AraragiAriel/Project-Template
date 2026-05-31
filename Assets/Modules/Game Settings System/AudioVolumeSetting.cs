using UnityEngine;

[CreateAssetMenu(fileName = "Audio Volume Setting", menuName = "ScriptableObject/Game Settings/Audio Volume")]
public class AudioVolumeSetting : GameSetting
{
    public override void Load()
    {
        data.bgmVolume = PlayerPrefs.GetFloat("bgmVolume", 1f);
        data.sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1f);
    }

    public override void Apply()
    {
    }

    public override void Save()
    {
        PlayerPrefs.SetFloat("bgmVolume", data.bgmVolume);
        PlayerPrefs.SetFloat("sfxVolume", data.sfxVolume);
    }
}
