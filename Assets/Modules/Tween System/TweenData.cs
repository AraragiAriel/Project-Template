using UnityEngine;
using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;

// [CreateAssetMenu(fileName = "Tween Data", menuName = "ScriptableObject/Tweens/Tween Data")]
public abstract class TweenData : ScriptableObject
{
    public abstract IEnumerator Tween(Parameters parameters);
    public abstract void SetEnd(Parameters parameters);

    public enum Type
    {
        Forward,
        Reverse,
        Loop,
    }

    [System.Serializable]
    public class Parameters
    {
        public TweenData data;
        public Component target;
        public Type type;

        public Parameters(){}

        public Parameters(Component component)
        {
            target = component;
        }

        public Parameters Clone() => (Parameters)MemberwiseClone();
    }
}

public abstract class TweenData<T> : TweenData where T : Component
{
    public override IEnumerator Tween(Parameters parameters)
    {
        if(parameters.target != null && parameters.target is T)
            yield return CoroutineManager.instance.StartCoroutine(Perform(parameters));
    }

    public override void SetEnd(Parameters parameters)
    {
        if(parameters.target != null && parameters.target is T)
            SetEndInternal(parameters);
    }

    protected abstract IEnumerator Perform(Parameters parameters);

    protected virtual void SetEndInternal(Parameters parameters){}
}