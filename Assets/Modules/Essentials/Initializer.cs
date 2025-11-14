using UnityEngine;
using DG.Tweening;

# if UNITY_EDITOR
using UnityEditor;
# endif

public static class Initializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    # if UNITY_EDITOR
    [InitializeOnLoadMethod]
    # endif
    private static void Initialize(){
        Res.Initialize();
        GameStateManager.Initialize();
        DOTween.SetTweensCapacity(1250, 125);

        // POPULATE DICTS
        Res.data.uidsData.Populate();
        Res.data.localizationData.Populate();
    }
}
