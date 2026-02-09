using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(ButtonStyleData), true)]
[CanEditMultipleObjects]
public class ButtonStyleDataEditor : Editor
{
    public override void OnInspectorGUI(){
        serializedObject.Update();

        var data = target as ButtonStyleData;

        List<string> toOmit = new();
        switch (data.transition)
        {
            case Selectable.Transition.None:
                toOmit.Add("colors");
                toOmit.Add("spriteState");
                toOmit.Add("animationTriggers");
                break;
            case Selectable.Transition.ColorTint:
                toOmit.Add("spriteState");
                toOmit.Add("animationTriggers");
                break;
            case Selectable.Transition.SpriteSwap:
                toOmit.Add("colors");
                toOmit.Add("animationTriggers");
                break;
            case Selectable.Transition.Animation:
                toOmit.Add("colors");
                toOmit.Add("spriteState");
                break;
        }
        DrawPropertiesExcluding(serializedObject, toOmit.ToArray());

        serializedObject.ApplyModifiedProperties();
    }
}
