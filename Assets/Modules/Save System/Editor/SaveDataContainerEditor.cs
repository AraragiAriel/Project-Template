using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveDataContainer))]
public class SaveDataContainerEditor : Editor {
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        var save = (SaveDataContainer)target;

        EditorGUILayout.BeginHorizontal();

            float height = 40f;
            GUI.backgroundColor = Color.green;
            if(GUILayout.Button("SAVE", GUILayout.Height(height)))
            {
                save.Save();
            }
            GUI.backgroundColor = Color.blue;
            if(GUILayout.Button("LOAD", GUILayout.Height(height)))
            {
                save.Load();
            }
            GUI.backgroundColor = Color.yellow;
            if(GUILayout.Button("RESET", GUILayout.Height(height)))
            {
                save.data = new SaveData();
            }
            GUI.backgroundColor = Color.red;
            if(GUILayout.Button("DELETE", GUILayout.Height(height)))
            {
                save.Delete();
            }

        EditorGUILayout.EndHorizontal();

        EditorUtility.SetDirty(save);
    }
}
