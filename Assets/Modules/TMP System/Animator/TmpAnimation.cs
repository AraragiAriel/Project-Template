using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class TmpAnimation : ScriptableObject
{
    public string id;

    public void Animate(TMP_CharacterInfo c, TmpAnimator animator)
    {
        if(!c.isVisible)
            return;

        AnimateCharacter(c, animator);
    }

    protected abstract void AnimateCharacter(TMP_CharacterInfo c, TmpAnimator animator);
}
