using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonEnabler : MonoBehaviour
{
    private List<RID> rids = new();

    private Button _button;
    private Button button => _button ??= GetComponent<Button>();

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
