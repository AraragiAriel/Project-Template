using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveTrigger : MonoBehaviour
{
    private void Awake(){
        ResourcesSystem.data.currentSave.saveSO.Load();
    }

    private void OnEnable(){
        StaticActions.OnSceneUnload += SceneConnect;

        #if UNITY_EDITOR
        EditorApplication.playModeStateChanged += PlayModeChange;
        #endif
    }

    private void OnDisable(){
        StaticActions.OnSceneUnload -= SceneConnect;

        #if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= PlayModeChange;
        #endif
    }

    private void OnApplicationQuit(){
        Save();
    }
    private void SceneConnect(SceneType scene) => Save();

    private void Save(){
        if(SceneChanger.currentScene == SceneType.Menu)
            return;

        StartCoroutine(SaveCo());
    }

    private IEnumerator SaveCo(){
        yield return new WaitForEndOfFrame();
        ResourcesSystem.data.currentSave.saveSO.Save();
    }

    #if UNITY_EDITOR
    private void PlayModeChange(PlayModeStateChange state){
        if(state == PlayModeStateChange.ExitingPlayMode)
            Save();        
    }
    #endif
}
