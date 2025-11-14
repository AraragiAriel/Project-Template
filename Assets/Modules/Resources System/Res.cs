using UnityEngine;

public static class Res
{
    public static ResourcesData data;
    public static SaveData save => data.currentSave.saveContainer.data;

    public static void Initialize(){
        data = Resources.Load("Resources Data") as ResourcesData;
    }
}
