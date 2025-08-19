using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(StatsOffsetData), true)]
public class StatsOffsetDataEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        var script = target as StatsOffsetData;

        if(GUILayout.Button("Reset Offsets")){
            foreach(var statOffset in script.offsets)
                statOffset.offset = 0f;
            EditorUtility.SetDirty(script);
        }
    }
}
