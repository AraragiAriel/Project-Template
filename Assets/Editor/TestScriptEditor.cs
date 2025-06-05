using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestScript), true)]
public class TestScriptEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        TestScript script = target as TestScript;

        if(GUILayout.Button("Func1")){
            script.Func1();
        }
        if(GUILayout.Button("Func2")){
            script.Func2();
        }
        if(GUILayout.Button("Func3")){
            script.Func3();
        }
    }
}
