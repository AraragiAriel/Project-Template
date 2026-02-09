using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private List<Tooltip> tooltips = new();
    private ITooltip iTooltip;
    private const int stop = 10;

    private RectTransform _rect;
    private RectTransform rect => _rect ??= GetComponent<RectTransform>();

    private List<RectTransform> activeRects =>
        new[]{rect}
        .Concat(tooltips.Select(t => t.tooltipRect))
        .ToList();

    public void OnPointerEnter(PointerEventData eventData) => Select(iTooltip);
    public void OnPointerExit(PointerEventData eventData) => Deselect();

    private void Awake(){
        iTooltip = GetComponent<ITooltip>();
        if(iTooltip == null){
            enabled = false;
            return;
        }

        tooltips.Clear();
    }

    private void Select(ITooltip iTooltip){
        if(activeRects.Count - 1 >= stop)
            return;

        try{
            var data = iTooltip.TooltipData();
            var tooltip = Instantiate(data.prefab);

            data.rects = activeRects;
            tooltip.Set(data);
            tooltips.Add(tooltip);
            
            foreach(var subTooltip in data.subTooltips)
                Select(subTooltip);            
        } catch(Exception e) {
            Debug.LogWarning($"Couldn't add tooltip: {e.Message}\n{e.StackTrace}");
        }
    }

    private void Deselect(){
        if(tooltips.Count == 0) return;

        foreach(int i in (tooltips.Count - 1).To(0)){
            if(tooltips[i] == null) continue;

            tooltips[i].Deselect();
        }
        tooltips.Clear();
    }

    private void OnDestroy(){
        Deselect();
    }
}

public class TooltipData{
    public Tooltip prefab;
    public List<RectTransform> rects = new();
    public List<ITooltip> subTooltips = new();
    public TooltipReplacer replacer = new();
}

public class TooltipReplacer{
    private Dictionary<string, string> placeholders = new();
    public void Add(string key, string value){
        if(placeholders.ContainsKey(key))
            placeholders[key] = value;
        else
            placeholders.Add(key, value);
    }

    public string Replace(string s){
        foreach (var kvp in placeholders)
            s = s.Replace("{" + kvp.Key + "}", kvp.Value);
        s = Res.data.colorTags.Parse(s);
        return s;
    }
}

public interface ITooltip{
    public TooltipData TooltipData();
}
