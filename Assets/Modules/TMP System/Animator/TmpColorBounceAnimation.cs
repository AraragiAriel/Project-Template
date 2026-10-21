using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Bounce Data", menuName = "ScriptableObject/UI/TMP Animations/Color Bounce")]
public class TmpColorBounceAnimation : TmpAnimation
{
    [Header("Colors")]
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private float speed;
    [SerializeField] private float sparse;
    [SerializeField] private bool perLetter;

    public override void AnimateCharacter(TMP_CharacterInfo c, TmpAnimator animator)
    {
        int vertexId = c.vertexIndex;
        int materialId = c.materialReferenceIndex;
        Color32[] colors = animator.text.textInfo.meshInfo[materialId].colors32;

        foreach(int i in 0.To(3))
        {
            colors[vertexId + i] = Color.Lerp(
                color1,
                color2,
                (Time.time*speed + (vertexId + (perLetter ? 0 : i))*sparse).MirroredFraction()
            );
        }
    }
}
