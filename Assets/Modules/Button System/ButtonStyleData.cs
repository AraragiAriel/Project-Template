using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Button Style Data", menuName = "ScriptableObject/Button Style Data")]
public class ButtonStyleData : ScriptableObject
{
    public Sprite sprite;

    [Space(20)]

    public Selectable.Transition transition;
    [Space]
    public ColorBlock colors;
    public SpriteState spriteState;
    public AnimationTriggers animationTriggers;
}