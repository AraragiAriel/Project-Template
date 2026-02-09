using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ButtonStyler), true)]
public class ButtonStylerEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        var script = target as ButtonStyler;

        GUILayout.Space(16);

        if(GUILayout.Button("APPLY STYLE"))
        {
            script.ApplyStyle();
            EditorUtility.SetDirty(script);
        }
        if(GUILayout.Button("READ STYLE"))
        {
            script.ReadStyle();
            if(script.data != null)
                EditorUtility.SetDirty(script.data);
        }
    }
}