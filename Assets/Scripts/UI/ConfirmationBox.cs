using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using DG.Tweening;

public struct ConfirmationBoxParameters{
    public string description;
    public Action actionIfTrue;
    public Action actionIfFalse;

    public ConfirmationBoxParameters(string description, Action actionIfTrue, Action actionIfFalse = null){
        this.description = description;
        this.actionIfTrue = actionIfTrue;
        this.actionIfFalse = actionIfFalse;
    }
}

public class ConfirmationBox : MonoBehaviour
{
    public static void OpenBox(ConfirmationBoxParameters parameters){
        Instantiate(Res.data.confirmationBox, Vector3.zero, Quaternion.identity).SetBox(parameters);
    }

    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration;
    private Action actionIfTrue, actionIfFalse;
    private bool finished = false;

    public void SetBox(ConfirmationBoxParameters parameters){
        description.text = parameters.description;
        actionIfTrue = parameters.actionIfTrue;
        actionIfFalse = parameters.actionIfFalse;

        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, fadeDuration);
    }

    public void Confirm(bool b){
        if(finished)
            return;
        finished = true;

        if(b)
            actionIfTrue?.Invoke();
        else
            actionIfFalse?.Invoke();

        canvasGroup.DOFade(0f, fadeDuration).onComplete = () => Destroy(gameObject);
    }
}
