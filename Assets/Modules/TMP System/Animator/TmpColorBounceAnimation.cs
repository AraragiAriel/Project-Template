using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Bounce Data", menuName = "ScriptableObject/TMP Animations/Color Bounce")]
public class TmpColorBounceAnimation : TmpAnimation
{
    [Header("Colors")]
    [SerializeField] private Color color1 = Color.blue;
    [SerializeField] private Color color2 = Color.red;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float sparse = .1f;
    [SerializeField] private bool perLetter = true;

    protected override void AnimateCharacter(TMP_CharacterInfo c, TmpAnimator animator)
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
