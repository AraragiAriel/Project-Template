using UnityEngine;

public abstract class ConstantData<T> : ScriptableObject
{
    public T value;

    public static implicit operator T(ConstantData<T> data)
        => data != null && data.value != null ? data.value : default;
}
