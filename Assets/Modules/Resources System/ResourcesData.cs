using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resources Data", menuName = "ScriptableObject/Others/ResourcesData")]
public partial class ResourcesData : ScriptableObject
{
    private Dictionary<string, Object> resources = new();

    public T GetResource<T>(string name) where T : Object{
        if(resources.TryGetValue(name, out var obj))
            return obj as T;
        return null;
    }

    #if UNITY_EDITOR
    public void RegisterResource(string name){
        if (!resources.ContainsKey(name))
            resources[name] = null;
    }
    #endif
}
