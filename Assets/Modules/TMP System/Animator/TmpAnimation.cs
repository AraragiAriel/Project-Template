using TMPro;
using UnityEngine;

public abstract class TmpAnimation : ScriptableObject
{
    public string id;

    public abstract void AnimateCharacter(TMP_CharacterInfo c, TmpAnimator animator);
}
