using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{   
    [SerializeField] private CanvasGroup mainScreen, settingsScreen, saveScreen, creditsScreen;
    // [SerializeField] private ParticleSystem ps1, ps2;

    [Header("Tween")]
    [SerializeField] private float duration;
    [SerializeField] private float fadeOutMult;
    [SerializeField] private Ease ease;

    [Header("Strings")]
    [SerializeField] private string twitterLink;

    private void Start(){
        CloseAll(true);
        FadeIn(mainScreen, true);
    }

    public void OpenMainScreen(bool insta = false){
        CloseAll(insta);
        FadeIn(mainScreen, insta);
    }

    public void OpenSaveScreen(bool insta = false){
        CloseAll(insta);
        FadeIn(saveScreen, insta);
    }

    public void OpenSettingsScreen(bool insta = false){
        CloseAll(insta);
        FadeIn(settingsScreen, insta);
    }

    public void OpenCreditsScreen(bool insta = false){
        CloseAll(insta);
        FadeIn(creditsScreen, insta);
    }

    private void CloseAll(bool insta = false){
        FadeOut(mainScreen, insta);
        FadeOut(saveScreen, insta);
        FadeOut(settingsScreen, insta);
        FadeOut(creditsScreen, insta);
    }

    private void FadeIn(CanvasGroup canvasGroup, bool insta = false){
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        canvasGroup.DOKill();
        if(!insta)
            canvasGroup.DOFade(1f, duration).SetEase(ease);
        else
            canvasGroup.alpha = 1f;
    }

    private void FadeOut(CanvasGroup canvasGroup, bool insta = false){
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvasGroup.DOKill();
        if(!insta)
            canvasGroup.DOFade(0f, duration*fadeOutMult).SetEase(ease)
                .onComplete = () => canvasGroup.gameObject.SetActive(false);
        else {
            canvasGroup.alpha = 0f;
            canvasGroup.gameObject.SetActive(false);
        }
    }

    public void OpenTwitter(){
        Application.OpenURL(twitterLink);
    }

    public void QuitGame(){
        if(mainScreen.interactable){
            ConfirmationBox.OpenBox(new ConfirmationBox.Parameters(Res.data.localizationData.Get("Confirm Quit"), ConfirmQuit));
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
