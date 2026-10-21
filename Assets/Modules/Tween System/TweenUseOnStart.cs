using UnityEngine;

public class TweenUseOnStart : TweenUse
{
    void Start()
    {
        StartCoroutine(Tween());
    }
}
