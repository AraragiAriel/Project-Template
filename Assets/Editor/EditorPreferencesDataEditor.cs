using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EditorPreferencesData), true)]
public class EditorPreferencesDataEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        GUILayout.Space(16);
        
        GUILayout.Label("OPEN FOLDERS");

        GUILayout.BeginHorizontal();
            if(GUILayout.Button("Project\nFolder")){
                Application.OpenURL(StaticData.projectFolder);
            }
            if(GUILayout.Button("Custom\nFolder")){
                Application.OpenURL(StaticData.customFolder);
            }
            if(GUILayout.Button("Persistent\nSave Path")){
                Application.OpenURL(SaveDataContainer.persistentPath);
            }
        GUILayout.EndHorizontal();

        GUILayout.Space(12);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontStyle = FontStyle.Bold;
        buttonStyle.fontSize = 16;
        if(GUILayout.Button("GLOBAL SET", buttonStyle, GUILayout.Height(32))){
            EditorTools.SetStats();
            EditorTools.SetUIDs();
        }
    }
}
