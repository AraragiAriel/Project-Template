using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "UIDs", menuName = "ScriptableObject/Others/UIDs")]
public class UIDs : ScriptableObject
{
    [Resource(ResourceAttribute.Tag.Data)] public UIDs uidsData;
    
    public List<UIDAsset> uids;
    private Dictionary<string, UIDAsset> dict = new();

    public T Get<T>(string uid) where T : UIDAsset{
        try{
            return dict[uid] as T;
        } catch {
            Debug.LogWarning("UIDAsset not found");
            return null;
        }
    }

    public bool Contains(string uid) => dict.ContainsKey(uid);

    public void Populate(){
        dict.Clear();
        foreach(var asset in uids)
            try{
                dict.Add(asset.uid, asset);
            } catch {
                Debug.LogWarning($"duplicate UID Asset: {asset.name}");
            }
    }

    private void OnValidate(){
        Populate();
    }
}
