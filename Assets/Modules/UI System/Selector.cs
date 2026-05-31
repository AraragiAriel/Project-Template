using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Linq;

public class Selector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;

    public Action<int> OnIdChange;
    private List<LocalizedString> localizedStrings = new();

    private int _currentId = 0;
    public int currentId
    {
        get => _currentId;
        set
        {
            _currentId = Mathf.Clamp(value, 0, count - 1);
            OnIdChange?.Invoke(_currentId);
        }
    }
    
    private int _count = 0;
    private int count{
        get => _count;
        set
        {
            _count = value;
            currentId = currentId;
        }
    }

    private void OnEnable()
    {
        StaticActions.OnGameSettingsChange += SetDisplay;
    }

    private void OnDisable()
    {
        StaticActions.OnGameSettingsChange -= SetDisplay;        
    }

    private void Start()
    {
        SetDisplay();
    }

    public void Set(List<string> strings, int initialId = 0) =>
        Set(strings.Cast<LocalizedString>().ToList(), initialId);

    public void Set(List<LocalizedString> localizedStrings, int initialId = 0)
    {
        this.localizedStrings = localizedStrings;
        count = this.localizedStrings.Count;
        currentId = initialId;
        SetDisplay();
    }

    public void Move(bool next)
    {
        currentId = currentId.Loop(next ? 1 : -1, count - 1);
        SetDisplay();
    }

    private void SetDisplay()
    {
        if(count == 0){
            tmp.text = "";
            return;
        }

        tmp.Set(localizedStrings[currentId]);
    }
}
