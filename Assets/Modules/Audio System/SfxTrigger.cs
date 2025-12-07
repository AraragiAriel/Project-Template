using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxTrigger : MonoBehaviour
{
    [SerializeField] private ClipsData clipsData;
    // [SerializeField] private bool atPosition = true;

    public void Trigger(int index){
        if(index >= clipsData.clips.Count || index < 0)
            return;

        // if(atPosition)
        //     AudioManager.PlayClip(clipsData.clips[index], transform.position);
        // else
            AudioManager.Play(new AudioManager.Parameters(clipsData.clips[index]));
    }
}