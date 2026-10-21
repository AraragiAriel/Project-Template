using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public interface IDataList<T> where T : Object
{
    public List<T> List();

    public void Populate()
    {
        string path = "Assets";
        string filter = $"t:{typeof(T).Name}";

        List<T> objs =
            AssetDatabase.FindAssets(filter, new[] {path})
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<T>)
            .Where(asset => asset != null)
            .ToList();
        
        var list = List();
        
        if(list.TrimNull())
            EditorUtility.SetDirty((Object)this);

        foreach(var obj in objs)
            if(!list.Contains(obj))
            {
                list.Add(obj);
                EditorUtility.SetDirty((Object)this);
            }
    }
}
