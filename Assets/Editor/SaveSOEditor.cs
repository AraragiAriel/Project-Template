using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveSO))]
public class SaveSOEditor : Editor {
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        SaveSO save = (SaveSO)target;

        // SAVE SYSTEM
        EditorGUILayout.BeginHorizontal();

            float height = 40f;
            if(GUILayout.Button("Save", GUILayout.Height(height))){
                save.Save();
            }
            if(GUILayout.Button("Reset", GUILayout.Height(height))){
                save.data = new SaveData();
            }
            if(GUILayout.Button("Load", GUILayout.Height(height))){
                save.Load();
            }
            if(GUILayout.Button("Delete", GUILayout.Height(height))){
                save.Delete();
            }

        EditorGUILayout.EndHorizontal();

        EditorUtility.SetDirty(save);
    }
}
