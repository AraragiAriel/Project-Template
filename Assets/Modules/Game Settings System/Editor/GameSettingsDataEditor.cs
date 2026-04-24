using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameSettingsData), true)]
public class GameSettingsDataEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        GUILayout.Space(16);

        GUI.backgroundColor = Color.red;
        if(GUILayout.Button("CLEAR PLAYER PREFS"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}