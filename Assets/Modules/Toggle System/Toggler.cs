using System.Collections.Generic;
using UnityEngine;

public abstract class Toggler : MonoBehaviour
{
    public List<ToggleUnit> units = new();

    void Awake()
    {
        if(units.Count == 0)
        {
            enabled = false;
            return;
        }
    }

    protected void Reset() => units.ForEach(u => u.Toggle(false));
}
