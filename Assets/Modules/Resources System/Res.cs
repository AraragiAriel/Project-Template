using System;
using UnityEngine;

public static class Res
{
    private static ResourcesData _data;
    public static ResourcesData data
    {
        get
        {
            if(_data == null)
                Initialize();
            return _data;
        }
        set => _data = value;
    }

    public static SaveData save => data.currentSave.saveContainer.data;

    public static void Initialize()
    {
        try
        {
            data = Resources.Load("Resources Data") as ResourcesData;
        }
        catch (Exception e)
        {
            Debug.Log($"{e.StackTrace}");
        }
    }
}
