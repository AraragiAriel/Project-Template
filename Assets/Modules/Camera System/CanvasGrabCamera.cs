using UnityEngine;

public class CanvasGrabCamera : MonoBehaviour
{
    private void OnEnable(){
        StaticActions.OnSceneChange += SetCamera;
    }

    private void OnDisable(){
        StaticActions.OnSceneChange -= SetCamera;        
    }

    private void Start(){
        SetCamera(SceneType.None);
    }

    private void SetCamera(SceneType scene){
        GetComponent<Canvas>().worldCamera = UICameraRef.uiCamera;
    }
}
