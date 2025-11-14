using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene Persistent Data", menuName = "ScriptableObject/Others/Scene Persistent Data")]
public class ScenePersistentData : ScriptableObject
{
    [Resource(ResourceAttribute.Tag.Data)] public ScenePersistentData scenePersistentData;

    public void Initialize(){

    }
}
