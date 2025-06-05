using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Odin.OdinSerializer;
using Odin.OdinSerializer.Utilities;

[System.Serializable]
[CreateAssetMenu(fileName = "SaveData ", menuName = "ScriptableObject/Others/SaveData")]
public class SaveSO : ScriptableObject
{
    public string saveName;
    public SaveData data;

    public void Create(){
        data = new SaveData();
        Save();
    }

    public void Save(){
        byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.Binary);
        File.WriteAllBytes(tempPath, bytes);

        if(File.Exists(path))
            File.Replace(tempPath, path, backupPath); 
        else        
            File.Move(tempPath, path);
    }

    public void Load(){
        if(!File.Exists(path) && !File.Exists(backupPath)){
            data = new SaveData();
            return;
        }

        if(!Read(path))
            if(!Read(backupPath))
                Debug.LogError("Couldn't load save file");
    }

    private bool Read(string path){
        try{
            byte[] bytes = File.ReadAllBytes(path);
            data = SerializationUtility.DeserializeValue<SaveData>(bytes, DataFormat.Binary);
            return true;
        } catch {
            return false;
        }
    }

    public void Delete(){
        if(File.Exists(path))
            File.Delete(path); 
        if(File.Exists(backupPath))
            File.Delete(backupPath);
    }

    public bool exists => File.Exists(path) || File.Exists(backupPath);
    public string path => PathFolder() + "/" + saveName + ".save";
    public static string persistentPath => Application.persistentDataPath;
    # if UNITY_EDITOR
    public static string editorPath = StaticData.customFolder + "/Dev Saves";
    #endif
    public string tempPath => path + ".tmp";
    public string backupPath => path + ".backup";

    public string PathFolder(){
        # if UNITY_EDITOR
        return editorPath;
        # else
        return persistentPath;
        # endif
    }
}
