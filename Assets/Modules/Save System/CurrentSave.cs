using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentSave ", menuName = "ScriptableObject/Others/CurrentSave")]
public class CurrentSave : ScriptableObject
{
    public SaveSO saveSO;

    // public SaveData Data(){
    //     return currentSave.data;
    // }

    // public void Save(){
    //     currentSave.Save();
    // }

    // public void Load(){
    //     currentSave.Load();
    // }

    // public void Delete(){
    //     currentSave.Load();
    // }
}
