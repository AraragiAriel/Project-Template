using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

[CustomEditor(typeof(MenuManager), true)]
public class MenuManagerEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        GUILayout.Space(8);
        
        var menuManager = target as MenuManager;

        if(GUILayout.Button("Main Menu")){
            menuManager.OpenMainScreen(true);
        }
        if(GUILayout.Button("Save Screen")){
            menuManager.OpenSaveScreen(true);
        }
        if(GUILayout.Button("Settings Screen")){
            menuManager.OpenSettingsScreen(true);
        }
        if(GUILayout.Button("Credits")){
            menuManager.OpenCreditsScreen(true);
        }
    }
}
