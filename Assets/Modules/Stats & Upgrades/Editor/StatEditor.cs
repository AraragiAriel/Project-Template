using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

[CanEditMultipleObjects]
[CustomEditor(typeof(Stat), true)]
public class StatEditor : Editor
{
    public override void OnInspectorGUI(){
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Previous")){
            ChangeSelection(false);
        }
        if(GUILayout.Button("Next")){
            ChangeSelection(true);            
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Space(8);

        base.OnInspectorGUI();

        Stat stat = target as Stat;

        if(GUILayout.Button("Initialize")){
            stat.Initialize();
        }

        GUILayout.Space(8);
        var texture = AssetPreview.GetAssetPreview(stat.icon);
        GUILayout.Label(texture, GUILayout.Width(80), GUILayout.Height(80));

        // int dimension = 40;
        // GUILayout.BeginHorizontal();
        //     foreach(UpgradeData upgrade in stat.iconPriority){
        //         if(upgrade == null)
        //             continue;
        //         if(upgrade.icon == null)
        //             continue;

        //         var smallTexture = AssetPreview.GetAssetPreview(upgrade.icon);
        //         GUILayout.Label(smallTexture, GUILayout.Width(dimension), GUILayout.Height(dimension));
        //     }
        // GUILayout.EndHorizontal();
    }

    private void ChangeSelection(bool next){
        Stat current = target as Stat;
        string directory = Path.GetDirectoryName(AssetDatabase.GetAssetPath(current));
        
        var assets = Directory.GetFiles(directory, "*.asset").
            Select(AssetDatabase.LoadAssetAtPath<Stat>).
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
