using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Data", menuName = "ScriptableObject/TMP Animations/Wave")]
public class TmpWaveAnimation : TmpAnimation
{
    [Header("Fluctuate")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float height = 1f;
    [SerializeField] private float sparse = .05f;

    protected override void AnimateCharacter(TMP_CharacterInfo c, TmpAnimator animator)
    {
        int vertexId = c.vertexIndex;
        int materialId = c.materialReferenceIndex;
        Vector3[] vertices = animator.text.textInfo.meshInfo[materialId].vertices;

        float offset = Mathf.Sin(Time.time*speed + vertexId*sparse)*height;
        foreach(int i in 0.To(3))
        {
            vertices[vertexId + i].y = animator.baseVertices[materialId][vertexId + i].y + offset;
        }
    }
}
