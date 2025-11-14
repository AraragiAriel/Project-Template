using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType{
    None = -1,
    Menu = 0,
    Main = 1,
}

public enum SceneChangeAnimType{
    None = 0,
}

public class SceneChanger : MonoBehaviour
{
    // STATIC

    public static readonly Util.BiDictionary<SceneType, string> scenesDict = new Util.BiDictionary<SceneType, string>{
        {SceneType.Menu,  "Menu"},
        {SceneType.Main,  "Main"},
    };

    public static SceneChanger instance;
    public static SceneType currentScene;

    // INSTANCE

    [SerializeField] private List<SceneChangeAnim> anims;    

    [HideInInspector] public bool duringSceneChange = false;

    private void Awake(){
        if(instance != null){
            Destroy(gameObject);
            return;
        } else {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        currentScene = scenesDict.Get(SceneManager.GetActiveScene().name);
    }

    private void Start(){
        duringSceneChange = false;
        StaticActions.OnSceneChange?.Invoke(currentScene);
    }
    
    public void ChangeScene(SceneType scene, SceneChangeAnimType anim = SceneChangeAnimType.None){
        if(duringSceneChange)
            return;

        StopAllCoroutines();
        StartCoroutine(ChangeSceneCo(scene, anim));
    }

    private IEnumerator ChangeSceneCo(SceneType scene, SceneChangeAnimType animType){
        SceneChangeAnim anim = anims.Find(a => a.type == animType);
        bool skipAnim = anim == null || animType == SceneChangeAnimType.None;
        duringSceneChange = true;
        StaticActions.OnSceneBeginChange?.Invoke(currentScene);

        if(!skipAnim)
            yield return StartCoroutine(anim.FadeIn());

        StaticActions.OnSceneUnload?.Invoke(currentScene);
        currentScene = scene;
        SceneManager.LoadScene(scenesDict.Get(scene));
        // var task = SceneManager.LoadSceneAsync(GetSceneName(data.scene));
        // task.allowSceneActivation = false;
        // while(task.progress < .9f)
        //     yield return new WaitForEndOfFrame();

        // task.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        duringSceneChange = false;
        StaticActions.OnSceneChange?.Invoke(currentScene);

        if(!skipAnim)
            yield return StartCoroutine(anim.FadeOut());
    }
}
