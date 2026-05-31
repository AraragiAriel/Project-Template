using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Jump Data", menuName = "ScriptableObject/TMP Animations/Jump")]
public class TmpJumpAnimation : TmpAnimation
{
    [Header("Fluctuate")]
    [SerializeField] private float jumpDuration = .25f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private int count = 4;

    protected override void AnimateCharacter(TMP_CharacterInfo c, TmpAnimator animator)
    {
        int vertexId = c.vertexIndex;
        int materialId = c.materialReferenceIndex;
        Vector3[] vertices = animator.text.textInfo.meshInfo[materialId].vertices;

        int characterID = Mathf.FloorToInt(vertexId/4f) % count;
        int currentID = Mathf.FloorToInt(Time.time/jumpDuration) % count;
        float height = (Time.time/jumpDuration*2f).MirroredFraction()*jumpHeight;

        foreach(int i in 0.To(3))
        {
            vertices[vertexId + i].y = animator.baseVertices[materialId][vertexId + i].y + (characterID == currentID ? height : 0f);
            // vertices[vertexId + i].y = animator.baseVertices[materialId][vertexId + i].y + jumpHeight;
        }
    }
}
