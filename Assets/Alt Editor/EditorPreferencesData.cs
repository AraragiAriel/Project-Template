#if UNITY_EDITOR

using UnityEngine;

// [CreateAssetMenu(fileName = "Editor Preferences Data", menuName = "ScriptableObject/Others/Editor Preferences")]
public class EditorPreferencesData : ScriptableObject
{
    [Header("Integration")]
    public bool disableSteam;
    public bool disableDiscord;

    [Header("Others")]
    public bool disableSave;
    public bool debugStateMachine;
}

#endif