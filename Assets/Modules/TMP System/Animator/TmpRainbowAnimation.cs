using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Rainbow Data", menuName = "ScriptableObject/UI/TMP Animations/Rainbow")]
public class TmpColorRainbowAnimation : TmpAnimation
{
    [Header("Colors")]
    [SerializeField] [Range(0f, 1f)] private float saturation;
    [SerializeField] [Range(0f, 1f)] private float value;
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
            colors[vertexId + i] = Color.HSVToRGB(
                (Time.time*speed + (vertexId + (perLetter ? 0 : i))*sparse) % 1,
                saturation,
                value
            );
        }
    }
}
