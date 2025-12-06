using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "CurrentSave ", menuName = "ScriptableObject/Others/CurrentSave")]
public class CurrentSave : ScriptableObject
{
    [Resource(ResourceAttribute.Tag.Data)] public CurrentSave currentSave;

    public SaveDataContainer saveContainer;
}
