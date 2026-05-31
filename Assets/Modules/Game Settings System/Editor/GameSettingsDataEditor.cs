using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameSettingsData), true)]
public class GameSettingsDataEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        GUILayout.Space(16);

        GUI.backgroundColor = Color.green;
        if(GUILayout.Button("SAVE SETTINGS"))
        {
            GameSettingsManager.Save();
        }
        GUI.backgroundColor = Color.blue;
        if(GUILayout.Button("LOAD SETTINGS"))
        {
            GameSettingsManager.Load();
            EditorUtility.SetDirty(Res.data.gameSettingsData);
        }
        GUI.backgroundColor = Color.red;
        if(GUILayout.Button("CLEAR PLAYER PREFS"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}