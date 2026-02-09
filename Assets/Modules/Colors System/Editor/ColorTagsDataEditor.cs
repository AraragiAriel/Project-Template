using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorTagsData))]
public class ColorTagsDataEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        var data = target as ColorTagsData;

        GUILayout.Space(16);

        foreach(var kvp in data.dict){
            var style = new GUIStyle(EditorStyles.label);
            style.normal.textColor = kvp.Value.GetColor().color;
            EditorGUILayout.LabelField(kvp.Value.GetColor().tag, style);
        }
    }
}
