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
        public float mult = 1f;
        public float angle = 55f;
        public float radius = .25f;
        public bool right = true;
        
        private Vector2? _step;
        public Vector2 step => _step??= radius*mult*Util.AngleToVector(right? angle : 180f - angle);
    }

    public static void Pop(Parameters param){
        Popup popup = Instantiate(Res.data.popup, param.pos, Quaternion.identity);
        popup.Set(param);
    }

    // INSTANCE

    [SerializeField] private TextMeshPro tmp;
    [SerializeField] private Ease moveEase, fadeEase;
    [SerializeField] private float duration;
    [SerializeField] private float height, scale;
    [SerializeField] private float fadeDelay;
    [SerializeField] private int vibrato;
    [SerializeField] private float elasticity;
    [SerializeField] private float jumpPower;
    
    private Parameters param;

    private void Set(Parameters param){
        this.param = param;
        tmp.Set(param.s);
        tmp.color = param.color;
        duration *= param.mult;
        
        StartCoroutine(TweenCo());
    }

    private IEnumerator TweenCo(){
        Transform t = transform;
        t.position = new Vector2(t.position.x, t.position.y);

        t.DOLocalJump(t.localPosition + Vector3.up*param.step.y, jumpPower*param.mult, 1, duration);
        t.DOMoveX(t.position.x +  param.step.x, duration).SetEase(moveEase);
        t.DOPunchScale(Vector3.one*scale*param.mult, duration, vibrato, elasticity).SetEase(moveEase);

        yield return new WaitForSeconds(duration*fadeDelay);

        tmp.DOFade(0f, duration*(1f - fadeDelay)).SetEase(fadeEase)
            .onComplete = () => Destroy(gameObject);
    }
}