using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{   
    [SerializeField] private CanvasGroup mainScreen, settingsScreen, saveScreen, creditsScreen;
    [SerializeField] private ParticleSystem ps1, ps2;

    [Header("Tween")]
    [SerializeField] private float duration;
    [SerializeField] private float fadeOutMult;
    [SerializeField] private Ease ease;

    [Header("Strings")]
    [SerializeField] private string twitterLink;

    private void Start(){
        FadeIn(mainScreen, true);
        FadeOut(saveScreen, true);
        FadeOut(settingsScreen, true);
        FadeOut(creditsScreen, true);
    }

    public void OpenMainScreen(){
        CloseAll();
        FadeIn(mainScreen);
    }

    public void OpenSaveScreen(){
        CloseAll();
        FadeIn(saveScreen);
    }

    public void OpenSettingsScreen(){
        CloseAll();
        FadeIn(settingsScreen);
    }

    public void OpenCreditsScreen(){
        CloseAll();
        FadeIn(creditsScreen);      
    }

    private void CloseAll(){
        FadeOut(mainScreen);
        FadeOut(saveScreen);
        FadeOut(settingsScreen);
        FadeOut(creditsScreen);
    }

    private void FadeIn(CanvasGroup canvasGroup, bool insta = false){
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        canvasGroup.DOKill();
        canvasGroup.DOFade(1f, insta ? 0f : duration).SetEase(ease);
    }

    private void FadeOut(CanvasGroup canvasGroup, bool insta = false){
        // canvasGroup.gameObject.SetActive(false);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvasGroup.DOKill();
        canvasGroup.DOFade(0f, insta ? 0f : duration*fadeOutMult).SetEase(ease);
    }

    public void OpenTwitter(){
        Application.OpenURL(twitterLink);
    }

    public void QuitGame(){
        if(mainScreen.interactable){
            ConfirmationBox.OpenBox(new ConfirmationBoxParameters(Res.String("Confirm Quit"), ConfirmQuit));
        } else {
            OpenMainScreen();
        }
    }

    private void ConfirmQuit(){
        Application.Quit();
    }

    public void OpenSteamPage(){
        try{
            Application.OpenURL(StaticData.steamAppPage);
        } catch {
            Application.OpenURL(StaticData.steamWebPage);
        }
    }
}
