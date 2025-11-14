#if UNITY_EDITOR

using UnityEngine;

[CreateAssetMenu(fileName = "Editor Preferences Data", menuName = "ScriptableObject/Others/Editor Preferences")]
public class EditorPreferencesData : ScriptableObject
{
    [Resource(ResourceAttribute.Tag.Editor)] public EditorPreferencesData editorPreferences;

    [Header("-")]
    public bool debugStateMachine;

    [Header("Integration")]
    public bool disableSteam;
    public bool disableDiscord;

    [Header("Others")]
    public bool disableMagnetTutorial;
    public bool disableComboUnlockScreen;
}

#endif