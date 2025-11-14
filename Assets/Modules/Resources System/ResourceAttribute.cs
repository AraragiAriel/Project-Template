using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field)]
public class ResourceAttribute : PropertyAttribute{
    public enum Tag{
        Demo,
        Data,
        Options,
        Prefab,
        UI,
        Tooltips,
        Others,
        Editor,
    }
    public Tag tag = Tag.Others;

    public ResourceAttribute(){}

    public ResourceAttribute(Tag tag){
        this.tag = tag;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ResourceAttribute))]
public class ResourceAttributeDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 0;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){}
}
#endif