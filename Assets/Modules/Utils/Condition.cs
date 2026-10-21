using UnityEngine;

public class Condition : MonoBehaviour
{
    public virtual bool Check() => true;

    public LocalizedString description;

    public static implicit operator bool (Condition condition)
        => condition != null ? condition.Check() : true;
}
