using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TMP Animations Data", menuName = "ScriptableObject/TMP Animations/Animations Data")]
public class TmpAnimationsData : ScriptableObject
{
    
    public List<TmpAnimation> animations;
    public Dictionary<string, TmpAnimation> dict = new();

    public void Populate()
    {
        dict.Clear();
        foreach(var anim in animations)
        {
            dict.Add(anim.id, anim);
        }
    }
}
