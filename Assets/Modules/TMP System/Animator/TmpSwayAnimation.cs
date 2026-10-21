using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Sway Data", menuName = "ScriptableObject/UI/TMP Animations/Sway")]
public class TmpSwayAnimation : TmpAnimation
{
    [Header("Fluctuate")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float angle = 1f;
    [SerializeField] private float sparse = .05f;

    public override void AnimateCharacter(TMP_CharacterInfo c, TmpAnimator animator)
    {
        int vertexId = c.vertexIndex;
        int materialId = c.materialReferenceIndex;
        Vector3[] vertices = animator.text.textInfo.meshInfo[materialId].vertices;

        Vector3 centerPoint = Vector2.zero;
        foreach(int i in 0.To(3))
            centerPoint += vertices[vertexId + i];
        centerPoint /= 4f;
        float currentAngle = Mathf.Sin(Time.time*speed*2*Mathf.PI + vertexId*sparse)*angle*Mathf.Deg2Rad;
        float sin = Mathf.Sin(currentAngle);
        float cos = Mathf.Cos(currentAngle);
        foreach(int i in 0.To(3))
        {
            Vector3 baseVertex = animator.baseVertices[materialId][vertexId + i] - centerPoint;
            vertices[vertexId + i] = new Vector3(
                baseVertex.x*cos - baseVertex.y*sin,
                baseVertex.y*cos + baseVertex.x*sin,
                baseVertex.z
            ) + centerPoint;
        }
    }
}
