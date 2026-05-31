using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Rainbow Data", menuName = "ScriptableObject/TMP Animations/Rainbow")]
public class TmpColorRainbowAnimation : TmpAnimation
{
    [Header("Colors")]
    [SerializeField] [Range(0f, 1f)] private float saturation = .8f;
    [SerializeField] [Range(0f, 1f)] private float value = 1f;
    [SerializeField] private float speed = .6f;
    [SerializeField] private float sparse = .02f;
    [SerializeField] private bool perLetter = true;

    protected override void AnimateCharacter(TMP_CharacterInfo c, TmpAnimator animator)
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
