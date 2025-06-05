using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteShadow), true)]
public class SpriteShadowEditor : Editor
{
    public override void OnInspectorGUI(){
        serializedObject.Update();

        SpriteShadow script = target as SpriteShadow;

        List<string> toOmit = new();
        if(!script.useOtherSr)
            toOmit.Add("otherSr");

        DrawPropertiesExcluding(serializedObject, toOmit.ToArray());
        
        serializedObject.ApplyModifiedProperties();
    }
}
