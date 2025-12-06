using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SceneChangeAnimDefault : SceneChangeAnim
{
    [Space]
    
    [SerializeField] private RectTransform bg;
    [SerializeField] private float xPos, duration;
    [SerializeField] private Ease easeIn, easeOut;
    [SerializeField] private ClipData clipIn, clipOut;

    public override IEnumerator FadeIn(){
        holder.SetActive(true);
        bg.anchoredPosition = new Vector2(-xPos, 0f);
        bg.DOLocalMoveX(0f, duration, true).SetEase(easeIn);
        AudioManager.Play(clipIn);
        yield return new WaitForSeconds(duration);        
    }

    public override IEnumerator FadeOut(){
        bg.DOLocalMoveX(xPos, duration, true).SetEase(easeOut);
        AudioManager.Play(clipOut);
        yield return new WaitForSeconds(duration);
        holder.SetActive(false);
    }
}
