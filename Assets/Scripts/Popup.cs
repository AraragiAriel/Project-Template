using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class Popup : MonoBehaviour
{
    public class Parameters{
        public Vector2 pos = Vector2.zero;
        public string s = "";
        public Color color = Color.white;
        public float durationMult = 1f;
        public Vector2 dir = Vector2.up;
    }

    public static void Pop(Parameters param){
        Popup popup = Instantiate(ResourcesSystem.data.popup, param.pos, Quaternion.identity);
        popup.Set(param);
    }

    // public static void Pop(Vector2 pos, string s, bool crit){
    //     Pop(pos, s, crit, ResourcesSystem.data.palette.white);
    // }

    // public static void Pop(Vector2 pos, string s, bool crit, Color color){
    //     var popup = Instantiate(ResourcesSystem.data.damagePopup, pos, Quaternion.identity);
    //     popup.Set(s, crit, color);
    // }

    // INSTANCE

    [SerializeField] private TextMeshPro tmp;
    [SerializeField] private Ease moveEase, fadeEase;
    [SerializeField] private float duration;
    [SerializeField] private float height, scale;
    [SerializeField] private float fadeDelay;
    [SerializeField] private int vibrato;
    [SerializeField] private float elasticity;
    private Parameters param;
    private float durationToUse;

    private void Set(Parameters param){
        this.param = param;
        tmp.text = param.s;
        tmp.color = param.color;
        durationToUse = param.durationMult*duration;
    }

    private void Start(){
        StartCoroutine(TweenCo());
    }

    private IEnumerator TweenCo(){
        Transform t = transform;
        t.position = new Vector2(t.position.x, t.position.y);

        t.DOMove((Vector2)t.position +  param.dir*height, durationToUse).SetEase(moveEase);
        t.DOPunchScale(Vector3.one*scale, durationToUse, vibrato, elasticity).SetEase(moveEase);

        yield return new WaitForSeconds(durationToUse*fadeDelay);

        tmp.DOFade(0f, durationToUse*(1f - fadeDelay)).SetEase(fadeEase)
            .onComplete = () => Destroy(gameObject);
    }
}
