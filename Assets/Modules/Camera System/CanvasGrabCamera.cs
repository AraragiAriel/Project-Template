using System;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Canvas))]
public class CanvasGrabCamera : MonoBehaviour
{
    private Canvas _canvas;
    private Canvas canvas => _canvas ??= GetComponent<Canvas>();

    private void OnEnable(){
        StaticActions.OnSceneChange += SceneChange;
    }

    private void OnDisable(){
        StaticActions.OnSceneChange -= SceneChange;        
    }

    private void Start(){
        Set();
    }

    private void SceneChange(SceneType scene) => Set();
    private void Set(){
        canvas.worldCamera = CameraManager.instance.Get(CameraManager.Type.UI);
        if(canvas.worldCamera == null)
            Util.Debug($"camera ref is null");
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if(!gameObject.activeInHierarchy || !gameObject.scene.IsValid() || !gameObject.scene.isLoaded)
            return;

        Set();
    }
#endif
}
