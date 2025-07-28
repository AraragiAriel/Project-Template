using UnityEngine;

public class OnLeaveGameDestroy : MonoBehaviour
{
    private void OnEnable(){
        StaticActions.OnSceneChange += SceneChange;
    }

    private void OnDestroy(){
        StaticActions.OnSceneChange -= SceneChange;        
    }

    private void SceneChange(SceneType scene){
        if(scene == SceneType.Menu)
            Destroy(gameObject);
    }
}
