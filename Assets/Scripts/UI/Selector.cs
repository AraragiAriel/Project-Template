using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Selector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    public Action<int> OnIdChange;
    private List<string> strings = new();
    private List<LocalizedString> localizedStrings = new();
    private bool useLs = false;

    private int _currentId = 0;
    public int currentId{
        get{return _currentId;}
        set{
            _currentId = value;
            if(_currentId >= count)
                _currentId = 0;
            if(_currentId < 0)
                _currentId = Mathf.Max(count - 1, 0);
            OnIdChange?.Invoke(_currentId);
        }
    }
    private int _count = 0;
    private int count{
        get{return _count;}
        set{
            _count = value;
            currentId = currentId;
        }
    }

    private void OnEnable(){
        StaticActions.OnGameSettingsChange += SetDisplay;
    }

    private void OnDisable(){
        StaticActions.OnGameSettingsChange -= SetDisplay;        
    }

    private void Start(){
        SetDisplay();
    }

    public void Set(List<string> strings, int initialId = 0){
        this.strings = new List<string>(strings);
        useLs = false;
        count = this.strings.Count;
        currentId = initialId;
        SetDisplay();
    }

    public void Set(List<LocalizedString> localizedStrings, int initialId = 0){
        this.localizedStrings = new List<LocalizedString>(localizedStrings);
        useLs = true;
        count = this.localizedStrings.Count;
        currentId = initialId;
        SetDisplay();
    }

    public void Move(bool next){
        currentId += next ? 1 : -1;
        SetDisplay();
    }

    private void SetDisplay(){
        if(count == 0){
            tmp.text = "";
            return;
        }

        if(useLs)
            tmp.Set(localizedStrings[currentId]);
        else
            tmp.Set(strings[currentId]);
    }
}
