using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorTagsData))]
public class ColorTagsDataEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        var data = target as ColorTagsData;

        GUILayout.Space(16);

        foreach(var kvp in data.dict)
        {
            var bgColor = kvp.Value.GetColor().color;
            Color.RGBToHSV(bgColor, out var _, out var _, out var v);
            var textColor = v > .5f ? Color.black : Color.white;

            EditorUtil.DrawLabel(kvp.Value.GetColor().tag.TagWrap("b"), bgColor, textColor, 16);
        }
    }
}
