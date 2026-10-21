using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Dance Data", menuName = "ScriptableObject/UI/TMP Animations/Dance")]
public class TmpDanceAnimation : TmpAnimation
{
    [Header("Dance")]
    [SerializeField] private float duration;
    [SerializeField] private List<Vector2> positions;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float sparse = .05f;

    public override void AnimateCharacter(TMP_CharacterInfo c, TmpAnimator animator)
    {
        int vertexId = c.vertexIndex;
        int materialId = c.materialReferenceIndex;
        Vector3[] vertices = animator.text.textInfo.meshInfo[materialId].vertices;

        float time = Time.time/duration + Mathf.FloorToInt(vertexId/4f)*sparse;
        int posID = Mathf.FloorToInt(time) % positions.Count;
        float percent = Mathf.Cos(2*Mathf.PI*time - Mathf.PI)*.5f + .5f;
        Vector2 translate = percent*positions[posID] + offset;

        foreach(int i in 0.To(3))
        {
            if(i == 0 || i == 3)
                continue;

            Vector2 baseVertex = (Vector2)animator.baseVertices[materialId][vertexId + i];
            vertices[vertexId + i] = baseVertex + translate;
        }
    }
}
