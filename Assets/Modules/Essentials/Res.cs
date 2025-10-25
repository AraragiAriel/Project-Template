using UnityEngine;

public static class Res
{
    public static ResourcesData data;
    public static SaveData save => data.currentSave.saveContainer.data;
    # if UNITY_EDITOR
    public static EditorPreferencesData editor => data.editorPreferences;
    # endif

    public static void Initialize(){
        data = Resources.Load("Resources Data") as ResourcesData;
    }

    public static LocalizedStringData String(string s){
        return Resources.Load("Strings/" + s) as LocalizedStringData;
    }
}
