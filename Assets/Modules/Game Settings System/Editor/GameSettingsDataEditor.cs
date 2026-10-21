using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameSettingsData), true)]
public class GameSettingsDataEditor : Editor
{
    void OnEnable()
    {
        GameSettingsManager.Load();
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        base.OnInspectorGUI();

        if (EditorGUI.EndChangeCheck())
        {
            GameSettingsManager.Save();
        }

        GUILayout.Space(16);

        GUI.backgroundColor = Color.red;
        if(GUILayout.Button("Reset"))
        {
            GameSettingsManager.Reset();
        }
    }
}