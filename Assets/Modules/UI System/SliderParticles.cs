using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderParticles : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform toFollow;
    [SerializeField] private ParticleSystem ps;
    private bool initialized = false;
    private Transform t;

    private void Awake(){
        t = transform;
        t.SetParent(null);
        t.localScale = Vector3.one;
        t.position = Vector3.zero;
    }

    private void OnEnable(){
        slider.onValueChanged.AddListener(ValueChange);
    }

    private void OnDisable(){
        slider.onValueChanged.RemoveListener(ValueChange);        
    }

    private void Start(){
        ps.Stop();
        StartCoroutine(DelayCo());
    }

    private void ValueChange(float value){
        if(!initialized)
            return;

        t.position = toFollow.position;
    }

    private IEnumerator DelayCo(){
        yield return new WaitForEndOfFrame();
        initialized = true;
        t.position = toFollow.position;
        ps.Play();
    }
}
