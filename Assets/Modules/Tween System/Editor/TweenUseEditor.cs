using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TweenUse), true)]
public class TweenUseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = target as TweenUse;

        GUILayout.Space(16);
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Tween"))
        {
            script.StartCoroutine(script.Tween());
        }
    }
}
