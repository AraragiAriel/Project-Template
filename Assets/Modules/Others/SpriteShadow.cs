using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteShadow : MonoBehaviour
{
    private const float dist = .25f;
    private Transform tParent, tChild;
    private SpriteRenderer srParent, srChild;

    [Header("Other Sprite Renderer")]
    public bool useOtherSr = false;
    [SerializeField] private SpriteRenderer otherSr;

    private void Awake(){
        tParent = transform;

        if(useOtherSr && otherSr != null)
            srParent = otherSr;
        else
            srParent = GetComponent<SpriteRenderer>();
        
        tChild = Instantiate(Res.data.spriteShadow, tParent.position, Quaternion.identity).transform;
        // tChild.parent = tParent;
        srChild = tChild.gameObject.GetComponent<SpriteRenderer>();
        SetSprite(srParent);
    }

    private void Start(){
        StartCoroutine(CheckChange());
    }

    private void OnEnable(){
        srParent.RegisterSpriteChangeCallback(SetSprite);
    }

    private void OnDisable(){
        srParent.UnregisterSpriteChangeCallback(SetSprite);
    }

    private IEnumerator CheckChange(){
        while(true){
            if(tParent.hasChanged){
                tChild.SetPositionAndRotation(tParent.position + new Vector3(0f, -dist, 1f), tParent.rotation);
                tChild.localScale = tParent.localScale;
                tParent.hasChanged = false;
            }

            if(srParent.color.a != srChild.color.a)
                SetSprite(srParent);

            yield return new WaitForFixedUpdate();
        }
    }

    private void SetSprite(SpriteRenderer sr){
        srChild.sprite = sr.sprite;
        Color aux = srChild.color;
        aux.a = sr.color.a;
        srChild.color = aux;
    }

    private void OnDestroy(){
        if(tChild != null)
            Destroy(tChild.gameObject);
    } 
}
