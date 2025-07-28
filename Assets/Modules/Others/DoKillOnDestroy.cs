using UnityEngine;
using DG.Tweening;

public class DoKillOnDestroy : MonoBehaviour
{
    private void OnDestroy(){
        DOTween.Kill(gameObject);

        // transform.DOKill();

        // if(TryGetComponent(out RectTransform rect)){
        //     rect.DOKill();
        // }
    }
}
