using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
#if UNITY_EDITOR || INCLUDE_DEBUG

    public KeyCode key1, key2, key3;

    private void OnDisable(){     
    }

    private void Start(){
    }

    private void Update(){
        if(Input.GetKeyDown(key1)){
            Func1();
        }
        if(Input.GetKeyDown(key2)){
            Func2();
        }
        if(Input.GetKeyDown(key3)){
            Func3();
        }
    }

    public void Func1(){
    }

    public void Func2(){      
    }

    public void Func3(){
    }

    private IEnumerator TestCo(){
        while(true){
            yield return new WaitForSeconds(1f);
            Debug.Log("Test Co");
        }
    }  

#endif
}
