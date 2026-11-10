using UnityEngine;
using UnityEditor;

public static class EditorTools
{
    #region FOLDERS

    [MenuItem("Tools/AraragiAriel/Folders/Project Folder")]
    private static void OpenProjectFolder()
    {
        Application.OpenURL(StaticData.projectFolder);
    }

    [MenuItem("Tools/AraragiAriel/Folders/Custom Folder")]
    private static void OpenCustomFolder()
    {
        Application.OpenURL(StaticData.customFolder);
    }

    [MenuItem("Tools/AraragiAriel/Folders/Persistent Save Path")]
    private static void OpenPersistentSavePath()
    {
        Application.OpenURL(SaveDataContainer.persistentPath);
    }

    #endregion

    #region SETTERS

    [MenuItem("Tools/AraragiAriel/Global Set")]
    private static void GlobalSet()
    {
        SetUIDs();
        SetLocalization();
        SetColors();
        SetStats();
    }

    private static void SetUIDs()
    {
        var data = Res.data.uids;

        foreach(UIDAsset uidAsset in EditorUtil.AssetsList<UIDAsset>())
            if (!data.uids.Contains(uidAsset))
            {
                var assetGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(uidAsset));
                uidAsset.uid ??= new();
                if(uidAsset.uid.id != assetGuid)
                {
                    uidAsset.uid.id = assetGuid;
                    EditorUtility.SetDirty(uidAsset);
                }

                data.uids.Add(uidAsset);
                EditorUtility.SetDirty(data);
            }
    }

    private static void SetLocalization()
    {
        var data = Res.data.localizationData;

        foreach(var asset in EditorUtil.AssetsList<ScriptableObject>())
            if(asset is ILocalizer && !data.localizers.Contains(asset))
            {
                data.localizers.Add(asset);
                EditorUtility.SetDirty(data);
            }
    }

    public static void SetColors()
    {
        var data = Res.data.colorTags;

        foreach(var asset in EditorUtil.AssetsList<ScriptableObject>())
            if(asset is IColor && !data.colors.Contains(asset))
            {
                data.colors.Add(asset);
                EditorUtility.SetDirty(data);
            }
    }

    private static void SetStats()
    {
        var data = Res.data.statsData;

        foreach(Stat stat in EditorUtil.AssetsList<Stat>())
            if(!data.stats.Contains(stat))
            {
                data.stats.Add(stat);
                EditorUtility.SetDirty(data);
            }
    }

    #endregion
}
