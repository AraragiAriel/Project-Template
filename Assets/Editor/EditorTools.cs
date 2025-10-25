using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.UI;

public static class EditorTools
{
    public static void SetStats(){
        var data = Res.data.statsData;
        data.stats.Clear();

        string path = "Assets/Data";
        string filter = "t:Stat";

        string[] assetGuids = AssetDatabase.FindAssets(filter, new[] {path});
        var stats = assetGuids
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<Stat>)
            .Where(asset => asset != null)
            .ToList();

        foreach(Stat stat in stats)
            data.stats.Add(stat);

        EditorUtility.SetDirty(data);
    }

    public static void SetUIDs(){
        var data = Res.data.uids;
        data.uids.Clear();

        string path = "Assets";
        string filter = "t:UIDAsset";

        string[] assetGuids = AssetDatabase.FindAssets(filter, new[] {path});
        var uidAssets = assetGuids
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<UIDAsset>)
            .Where(asset => asset != null)
            .ToList();

        foreach(UIDAsset uidAsset in uidAssets){
            // INITIALIZE UID
            var assetGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(uidAsset));
            if(uidAsset.uid == null)
                uidAsset.uid = new();
            if(uidAsset.uid.id != assetGuid){
                uidAsset.uid.id = assetGuid;
                EditorUtility.SetDirty(uidAsset);
            }

            data.uids.Add(uidAsset);
        }

        EditorUtility.SetDirty(data);
    }
}
