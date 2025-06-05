using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EditorPreferencesData), true)]
public class EditorPreferencesDataEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        GUILayout.Space(8);
        
        if(GUILayout.Button("Open Project Folder")){
            Application.OpenURL(StaticData.projectFolder);
        }
        if(GUILayout.Button("Open Custom Folder")){
            Application.OpenURL(StaticData.customFolder);
        }
        if(GUILayout.Button("Open Persistent Save Path")){
            Application.OpenURL(SaveSO.persistentPath);
        }
    }
}
