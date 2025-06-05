using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public enum SceneType{
    None = -1,
    Menu = 0,
    Store = 1,
    Main = 2,
}

public class SceneChanger : MonoBehaviour
{
    // STATIC

    public static readonly Dictionary<SceneType, string> scenesDict = new Dictionary<SceneType, string>{
        {SceneType.Menu,  "Menu"},
        {SceneType.Store, "Store"},
        {SceneType.Main,  "Main"},
    };

    public static string GetSceneName(SceneType type){
        return scenesDict[type];
    }

    public static SceneType GetSceneType(string sceneName){
        var result = scenesDict.FirstOrDefault(kvp => kvp.Value == sceneName);
        return result.Equals(default(KeyValuePair<SceneType, string>)) ? SceneType.None : result.Key;
    }

    public static SceneChanger instance;
    public static SceneType currentScene;

    // INSTANCE

    [SerializeField] private GameObject holder;
    [SerializeField] private RectTransform leftArrow1, leftArrow2, rightArrow1, rightArrow2;
    [SerializeField] private float xPos, duration;
    [SerializeField] private Ease easeIn, easeOut;
    [SerializeField] private ClipData clipIn, clipOut;

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

        currentScene = GetSceneType(SceneManager.GetActiveScene().name);
    }

    private void Start(){
        duringSceneChange = false;
        StaticActions.OnSceneChange?.Invoke(currentScene);
    }
    
    public void ChangeScene(SceneType scene){
        if(duringSceneChange)
            return;

        StopAllCoroutines();
        StartCoroutine(ChangeSceneCo(scene));
    }

    private IEnumerator ChangeSceneCo(SceneType scene){
        duringSceneChange = true;
        StaticActions.OnSceneBeginChange?.Invoke(currentScene, duration);

        // yield return StartCoroutine(FadeInCo());

        StaticActions.OnSceneUnload?.Invoke(currentScene);
        currentScene = scene;
        SceneManager.LoadScene(GetSceneName(scene));
        // var task = SceneManager.LoadSceneAsync(GetSceneName(data.scene));
        // task.allowSceneActivation = false;
        // while(task.progress < .9f)
        //     yield return new WaitForEndOfFrame();

        // task.allowSceneActivation = true;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        duringSceneChange = false;
        StaticActions.OnSceneChange?.Invoke(currentScene);

        // yield return StartCoroutine(FadeOutCo());
    }

    private IEnumerator FadeInCo(){
        holder.SetActive(true);
        leftArrow1.anchoredPosition = new Vector2(-xPos, 0f);
        leftArrow1.DOLocalMoveX(0f, duration, true).SetEase(easeIn);
        leftArrow2.anchoredPosition = new Vector2(-xPos, 0f);
        leftArrow2.DOLocalMoveX(0f, duration, true).SetEase(easeIn);
        rightArrow1.anchoredPosition = new Vector2(xPos, 0f);
        rightArrow1.DOLocalMoveX(0f, duration, true).SetEase(easeIn);
        rightArrow2.anchoredPosition = new Vector2(xPos, 0f);
        rightArrow2.DOLocalMoveX(0f, duration, true).SetEase(easeIn);
        AudioManager.PlayClip(clipIn);
        yield return new WaitForSeconds(duration);
    }

    private IEnumerator FadeOutCo(){
        leftArrow1.DOLocalMoveX(xPos, duration, true).SetEase(easeOut);
        leftArrow2.DOLocalMoveX(xPos, duration, true).SetEase(easeOut);
        rightArrow1.DOLocalMoveX(-xPos, duration, true).SetEase(easeOut);
        rightArrow2.DOLocalMoveX(-xPos, duration, true).SetEase(easeOut);
        AudioManager.PlayClip(clipOut);
        yield return new WaitForSeconds(duration);
        holder.SetActive(false);
    }
}
