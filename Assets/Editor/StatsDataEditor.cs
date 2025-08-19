using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(StatsData), true)]
public class StatsDataEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        StatsData script = target as StatsData;

        if(GUILayout.Button("Set")){
            string path = "Assets/Data/Stats";
            string filter = "t:Stat";

            string[] assetGuids = AssetDatabase.FindAssets(filter, new[] {path});
            var stats = assetGuids
                                .Select(AssetDatabase.GUIDToAssetPath)
                                .Select(AssetDatabase.LoadAssetAtPath<Stat>)
                                .Where(asset => asset != null)
                                .ToList();

            script.stats.Clear();
            foreach(Stat stat in stats)
                script.stats.Add(stat);

            EditorUtility.SetDirty(script);
        }

        int width = 10;
        int counter = 0;
        int dimension = 40;
        GUILayout.BeginHorizontal();
            foreach(Stat stat in script.stats){
                if(stat.GetIcon(true) == null)
                    continue;
                    
                if(counter % width == 0){
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                } 
                var texture = AssetPreview.GetAssetPreview(stat.GetIcon(true));
                GUILayout.Label(texture, GUILayout.Width(dimension), GUILayout.Height(dimension));
                counter++;
            }
        GUILayout.EndHorizontal();
    }
}
