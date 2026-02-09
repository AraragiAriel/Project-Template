using System.Collections;
using UnityEngine;

public class SequenceToggler : Toggler
{
    [SerializeField] private float duration;

    private void OnEnable(){
        StartCoroutine(FlickCo());
    }

    private IEnumerator FlickCo(){
        int current = -1;

        while(true){
            current = current.Loop(1, units.Count - 1);

            Reset();
            units[current].Toggle(true);

            yield return new WaitForSeconds(duration);
        }
    }
}
