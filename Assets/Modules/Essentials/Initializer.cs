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
        GameStateManager.Initialize();
        RID.Initialize();
        DOTween.SetTweensCapacity(1250, 125);
        Res.Initialize();
    }
}
