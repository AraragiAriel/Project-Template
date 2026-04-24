using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Toggler), true)]
public class TogglerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = target as Toggler;

        if(GUILayout.Button("FIND UNITS"))
        {
            script.units.Clear();
            script.units = script.transform.GetComponentsInChildren<ToggleUnit>().ToList();

            EditorUtility.SetDirty(script);
        }
        if(GUILayout.Button("REVERSE ORDER"))
        {
            script.units.Reverse();
        }
    }
}
