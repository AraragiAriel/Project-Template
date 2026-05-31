using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Button Style Data", menuName = "ScriptableObject/Button Style Data")]
public class ButtonStyleData : ScriptableObject
{
    public Sprite sprite;

    [Space(20)]

    [Header("TRANSITION")]
    public Selectable.Transition transition;
    [Space]
    public ColorBlock colors;
    [Space]
    public SpriteState spriteState;
    [Space]
    public AnimationTriggers animationTriggers;

    [Header("MOVE CONTENT ON CLICK")]
    public bool move;
    public int pixels;
}