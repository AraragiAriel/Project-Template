using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

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
            if(GUILayout.Button("APPLY\nSTYLE"))
            {
                script.ApplyStyle();
                if(script.TryGetComponent(out Image image))
                    EditorUtility.SetDirty(image);
                if(script.TryGetComponent(out Button button))
                    EditorUtility.SetDirty(button);
            }

            GUI.backgroundColor = Color.red;
            if(GUILayout.Button("READ\nSTYLE"))
            {
                script.ReadStyle();
                if(script.data != null)
                    EditorUtility.SetDirty(script.data);
            }
            
            GUI.backgroundColor = old;

        GUILayout.EndHorizontal();
    }
}