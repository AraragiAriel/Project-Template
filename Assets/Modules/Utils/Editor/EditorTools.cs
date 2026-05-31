using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.UI;

public static class EditorTools
{
    #region FOLDERS

    [MenuItem("Tools/AraragiAriel/Folders/Project Folder")]
    private static void OpenProjectFolder(){
        Application.OpenURL(StaticData.projectFolder);
    }

    [MenuItem("Tools/AraragiAriel/Folders/Custom Folder")]
    private static void OpenCustomFolder(){
        Application.OpenURL(StaticData.customFolder);
    }

    [MenuItem("Tools/AraragiAriel/Folders/Persistent Save Path")]
    private static void OpenPersistentSavePath(){
        Application.OpenURL(SaveDataContainer.persistentPath);
    }

    #endregion

    #region SETTERS

    [MenuItem("Tools/AraragiAriel/Global Set")]
    private static void GlobalSet(){
        SetStats();
        SetUIDs();
        SetLocalization();
        SetColors();
    }

    private static void SetStats(){
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

    private static void SetUIDs(){
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

    private static void SetLocalization(){
        var data = Res.data.localizationData;
        data.localizers.Clear();

        string path = "Assets";
        string filter = "t:ScriptableObject";

        string[] assetGuids = AssetDatabase.FindAssets(filter, new[] {path});
        var localizers = assetGuids
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>)
            .Where(asset => asset != null)
            .ToList();

        foreach(var asset in localizers){
            if(asset is ILocalizer){
                data.localizers.Add(asset);
            }
        }

        EditorUtility.SetDirty(data);
    }

    public static void SetColors(){
        var data = Res.data.colorTags;
        data.colors.Clear();

        string path = "Assets";
        string filter = "t:ScriptableObject";

        string[] assetGuids = AssetDatabase.FindAssets(filter, new[] {path});
        var colors = assetGuids
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>)
            .Where(asset => asset != null)
            .ToList();

        foreach(var asset in colors){
            if(asset is IColor){
                data.colors.Add(asset);
            }
        }
        data.Populate();

        EditorUtility.SetDirty(data);
    }

    #endregion
}
