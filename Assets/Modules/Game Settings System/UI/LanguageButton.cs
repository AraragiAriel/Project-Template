using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LanguageButton : MonoBehaviour
{
    [SerializeField] private GameLanguage language;
    [SerializeField] private CanvasGroup canvasGroup;
    
    [Header("Tween")]
    [SerializeField] private Ease ease;
    [SerializeField] private float alpha;
    [SerializeField] private float duration;

    private void OnEnable(){
        StaticActions.OnGameSettingsChange += GameSettingsChange;
    }

    private void OnDisable(){
        StaticActions.OnGameSettingsChange -= GameSettingsChange;        
    }

    private void Start(){
        GameSettingsChange();
    }

    public void Click(){
        Res.data.gameSettings.language = language;
        GameSettingsManager.instance.Apply();
    }

    private void GameSettingsChange(){
        bool current = language == Res.data.gameSettings.language;
        canvasGroup.DOKill();
        canvasGroup.DOFade(current ? 1f : alpha, duration).SetEase(ease);
    }
}
