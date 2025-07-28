using System.Collections;
using UnityEngine;

public abstract class SceneChangeAnim : MonoBehaviour
{
    public SceneChangeAnimType type;
    [SerializeField] protected GameObject holder;

    public abstract IEnumerator FadeIn();
    public abstract IEnumerator FadeOut();

    private void Start(){
        holder.SetActive(false);
    }
}
