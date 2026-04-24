using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    None = -1,
    Menu = 0,
    Main = 1,
}

public enum SceneChangeAnimType
{
    None = 0,
}

public class SceneChanger : MonoBehaviour
{
    // STATIC

    public static readonly Util.BiDictionary<SceneType, string> scenesDict = new Util.BiDictionary<SceneType, string>
    {
        {SceneType.Menu,  "Menu"},
        {SceneType.Main,  "Main"},
    };

    public static SceneChanger instance;
    public static SceneType currentScene;

    // INSTANCE

    [SerializeField] private List<SceneChangeAnim> anims;    

    [HideInInspector] public bool duringSceneChange = false;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        try
        {
            currentScene = scenesDict.Get(SceneManager.GetActiveScene().name);
        }
        catch
        {
            currentScene = SceneType.None;
        }
    }

    private void Start()
    {
        duringSceneChange = false;
        StaticActions.OnSceneChange?.Invoke(currentScene);
    }
    
    public void ChangeScene(SceneType scene, SceneChangeAnimType anim = SceneChangeAnimType.None)
    {
        if(duringSceneChange)
            return;

        StopAllCoroutines();
        StartCoroutine(ChangeSceneCo(scene, anim));
    }

    private IEnumerator ChangeSceneCo(SceneType scene, SceneChangeAnimType animType)
    {
        duringSceneChange = true;

        SceneChangeAnim prefab = anims.Find(a => a.type == animType);
        StaticActions.OnSceneBeginChange?.Invoke(currentScene);

        SceneChangeAnim anim = null;
        if(prefab != null)
            anim = Instantiate(prefab, transform);
        if(anim != null)
        {
            yield return StartCoroutine(anim.FadeIn());
        }
        StaticActions.OnSceneUnload?.Invoke(currentScene);
        currentScene = scene;
        // SceneManager.LoadScene(scenesDict.Get(scene));
        var task = SceneManager.LoadSceneAsync(scenesDict.Get(scene));
        task.allowSceneActivation = false;
        while(task.progress < .9f)
            yield return new WaitForEndOfFrame();

        task.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        StaticActions.OnSceneChange?.Invoke(currentScene);

        if(anim != null)
        {
            yield return StartCoroutine(anim.FadeOut());
            Destroy(anim.gameObject);
        }
        duringSceneChange = false;
    }
}
