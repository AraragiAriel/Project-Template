using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ButtonManager))]
public class ButtonEnabler : MonoBehaviour
{
    private List<RID> rids = new();

    private ButtonManager _button;
    private ButtonManager button => _button ??= GetComponent<ButtonManager>();

    public void AddUnit(RID rid)
    {
        if(rids.Contains(rid))
            return;

        rids.Add(rid);
        Set();
    }

    public void RemoveUnit(RID rid)
    {
        if(rids.Remove(rid))
            Set();
    }

    public void SetUnit(bool active, RID rid)
    {
        if(active)
            RemoveUnit(rid);
        else
            AddUnit(rid);
    }

    private void Set()
    {
        button.interactable = rids.Count == 0;
    }
}
