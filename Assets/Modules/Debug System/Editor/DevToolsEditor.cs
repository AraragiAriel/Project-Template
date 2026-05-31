using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DevTools))]
public class DevToolsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = target as DevTools;

        GUILayout.Space(24);

        GUILayout.BeginHorizontal();
            if(GUILayout.Button("SET\nFs"))
            {
                foreach(int i in 0.To(11))
                {
                    if(i >= script.slots.Count)
                        break;
                    script.slots[i].key = KeyCode.F1 + i;
                }
                EditorUtility.SetDirty(script);
            }
            if(GUILayout.Button("SET\nNUMBERS"))
            {
                foreach(int i in 0.To(8))
                {
                    if(i >= script.slots.Count)
                        break;
                    script.slots[i].key = KeyCode.Alpha1 + i;
                }
                EditorUtility.SetDirty(script);
            }
        GUILayout.EndHorizontal();
    }
}
