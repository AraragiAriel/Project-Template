using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ButtonStyler), true)]
public class ButtonStylerEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        var script = target as ButtonStyler;

        GUILayout.Space(16);

        GUILayout.BeginHorizontal();

            var old = GUI.backgroundColor;
            GUI.backgroundColor = Color.green;
            if(GUILayout.Button("READ\nSTYLE"))
            {
                script.ReadStyle();
                if(script.data != null)
                    EditorUtility.SetDirty(script.data);
            }

            GUI.backgroundColor = Color.red;
            if(GUILayout.Button("APPLY\nSTYLE"))
            {
                script.ApplyStyle();
                EditorUtility.SetDirty(script);
            }
            GUI.backgroundColor = old;

        GUILayout.EndHorizontal();
    }
}