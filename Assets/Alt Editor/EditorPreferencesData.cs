#if UNITY_EDITOR

using UnityEngine;

[CreateAssetMenu(fileName = "Editor Preferences Data", menuName = "ScriptableObject/Others/Editor Preferences")]
public class EditorPreferencesData : ScriptableObject
{
    [Header("Combat")]
    public bool disableMeleeDamage;
    public bool disableExpireDamage;
    public bool disableDeath;
    public bool disableShooting;
    public bool disableEnemySpawn;
    public bool enableOverloadWithoutChallenge;

    [Header("Integration")]
    public bool disableSteam;
    public bool disableDiscord;

    [Header("Others")]
    public bool disableMagnetTutorial;
    public bool disableComboUnlockScreen;
}

#endif