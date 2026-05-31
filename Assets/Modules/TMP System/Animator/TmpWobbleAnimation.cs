using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Wobble Data", menuName = "ScriptableObject/TMP Animations/Wobble")]
public class TmpWobbleAnimation : TmpAnimation
{
    [Header("Wobble")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private Vector2 dist = new(.2f, .2f);
    [SerializeField] private float sparse = .05f;

    protected override void AnimateCharacter(TMP_CharacterInfo c, TmpAnimator animator)
    {
        int vertexId = c.vertexIndex;
        int materialId = c.materialReferenceIndex;
        Vector3[] vertices = animator.text.textInfo.meshInfo[materialId].vertices;

        foreach(int i in 0.To(3))
        {
            Vector2 offset = new Vector2(
                Mathf.Cos(Time.time*speed + (vertexId + i)*sparse)*dist.x,
                Mathf.Sin(Time.time*speed + (vertexId + i)*sparse)*dist.y
            );
            vertices[vertexId + i] = (Vector2)animator.baseVertices[materialId][vertexId + i] + offset;
        }
    }
}
