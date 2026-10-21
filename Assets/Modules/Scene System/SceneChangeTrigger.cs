using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    [SerializeField] private SceneType scene;

    public void Trigger()
    {
        SceneChanger.instance.ChangeScene(scene);
    }
}
