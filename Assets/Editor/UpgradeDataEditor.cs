using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

[CustomEditor(typeof(UpgradeData), true)]
[CanEditMultipleObjects]
public class UpgradeDataEditor : Editor
{
    public override void OnInspectorGUI(){
        serializedObject.Update();

        UpgradeData data = target as UpgradeData;
        EditorUtility.SetDirty(target);

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Previous")){
            ChangeSelection(false);
        }
        if(GUILayout.Button("Next")){
            ChangeSelection(true);            
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(8);

        List<string> toOmit = new();
        if(data.stat == null){
            toOmit.Add("valuePerLevel");
            toOmit.Add("percent");
        }
        DrawPropertiesExcluding(serializedObject, toOmit.ToArray());

        if(data.stat != null){
            GUILayout.Space(8);

            EditorGUILayout.LabelField("Stat base value", data.stat.baseValue.ToString());
            EditorGUILayout.LabelField("Stat added value", (data.maxLevel*data.valuePerLevel).ToString());

            GUILayout.Space(8);

            EditorGUILayout.LabelField("Upgrade max price", data.initialCost.amount + " -> " + (data.initialCost.amount + (data.maxLevel-1)*data.costIncrease).ToString());
        }

        GUILayout.Space(8);
        var texture = AssetPreview.GetAssetPreview(data.icon);
        GUILayout.Label(texture);

        serializedObject.ApplyModifiedProperties();
    }
    
    private void ChangeSelection(bool next){
        UpgradeData current = target as UpgradeData;
        string directory = Path.GetDirectoryName(AssetDatabase.GetAssetPath(current));
        
        var assets = Directory.GetFiles(directory, "*.asset").
            Select(AssetDatabase.LoadAssetAtPath<UpgradeData>).
            Where(obj => obj != null).
            ToList();
        assets = assets.OrderBy(asset => asset.name).ToList();

        int id = assets.IndexOf(current);
        if(next){
            // Next
            if(id < assets.Count - 1)
                Selection.activeObject = assets[id + 1];
        } else {
            // Previous
            if(id >= 1)
                Selection.activeObject = assets[id - 1];
        }
    }
}
