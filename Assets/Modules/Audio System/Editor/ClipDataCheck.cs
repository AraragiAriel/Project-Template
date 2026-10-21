using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Clip Data Check", menuName = "ScriptableObject/Audio/Clip Data Check")]
public class ClipDataCheck : ScriptableObject
{
    [SerializeField] private List<ClipCheck> uncheckeds = new();
    [SerializeField] private List<ClipCheck> checkeds = new();

    void OnValidate()
    {
        Arrange();
    }

    private void Arrange()
    {
        Trim();

        List<ClipCheck> falseUncheckeds = uncheckeds.Where(cc => cc.check).ToList();
        List<ClipCheck> falseCheckeds = checkeds.Where(cc => !cc.check).ToList();

        if(falseUncheckeds.Count == 0 && falseCheckeds.Count == 0)
            return;

        foreach(var falseUnchecked in falseUncheckeds)
        {
            uncheckeds.Remove(falseUnchecked);
            checkeds.Add(falseUnchecked);
        }
        foreach(var falseChecked in falseCheckeds)
        {
            checkeds.Remove(falseChecked);
            uncheckeds.Add(falseChecked);
        }

        EditorUtility.SetDirty(this);
    }

    public void Scan()
    {
        var clips = EditorUtil.AssetsList<ClipData>("Audio");

        Trim();

        foreach(var clip in clips)
        {
            if(!uncheckeds.Any(cc => cc.data == clip) && !checkeds.Any(cc => cc.data == clip))
            {
                // NOT FOUND
                uncheckeds.Add(new ClipCheck(clip));
                EditorUtility.SetDirty(this);
            }
        }
    }

    private void Trim()
    {
        bool altered = false;
        altered = altered || uncheckeds.RemoveAll(cc => cc.data == null) > 0;
        altered = altered || checkeds.RemoveAll(cc => cc.data == null) > 0;

        if(altered)
            EditorUtility.SetDirty(this);
    }

    public bool Check(ClipData data) => checkeds.Any(cc => cc.data == data);

    [System.Serializable]
    private class ClipCheck
    {
        public ClipData data;
        public bool check = false;

        public ClipCheck(ClipData data)
        {
            this.data = data;
            check = false;
        }
    }
}