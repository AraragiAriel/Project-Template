using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ClipDataCheck), true)]
public class ClipDataCheckEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();
        
       var data = target as ClipDataCheck;

        if(GUILayout.Button("Scan"))
        {
            data.Scan();
        }
    }
}
