using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(UID))]
public class UIDDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
        SerializedProperty id = property.FindPropertyRelative("id");

        var target = property.serializedObject.targetObject;
        string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(target));
        if(id.stringValue != guid){
            id.stringValue = guid;
            property.serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        EditorGUI.PropertyField(position, property, label, true);
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label){
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
