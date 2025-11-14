using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangerTrigger : MonoBehaviour
{
    [SerializeField] private SceneType scene;

    public void Trigger(){
        SceneChanger.instance.ChangeScene(scene);
    }
}
