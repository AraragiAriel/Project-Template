using System.Collections;
using UnityEngine;

public class AlternateToggler : Toggler
{
    [SerializeField] private float duration;

    private void OnEnable(){
        StartCoroutine(FlickCo());
    }

    private IEnumerator FlickCo(){
        bool parity = false;

        while(true){
            parity = !parity;

            Reset();
            foreach(int i in 0.To(units.Count - 1))
                units[i].Toggle(i % 2 == 0 == parity);

            yield return new WaitForSeconds(duration);
        }
    }
}