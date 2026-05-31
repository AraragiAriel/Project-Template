using UnityEngine;
using System.Collections.Generic;

public class FpsSelector : MonoBehaviour
{
    [SerializeField] private GameObject fpsSlider;

    private bool setDone = false;

    private Selector _selector;
    private Selector selector => _selector ??= GetComponent<Selector>();

    private void OnEnable()
    {
        selector.OnIdChange += SwitchChange;
    }

    private void OnDisable()
    {
        selector.OnIdChange -= SwitchChange;        
    }

    private void Start()
    {
        List<LocalizedString> aux = new();
        foreach(var fpsType in Util.EnumList<FpsType>())
        {
            aux.Add(fpsType.Localize());
        }
        
        selector.Set(aux, (int)Res.data.gameSettingsData.fpsType);
        SetSlider();
        setDone = true;
    }

    private void SwitchChange(int id)
    {
        if(!setDone)
            return;
            
        Res.data.gameSettingsData.fpsType = (FpsType)id;
        SetSlider();
        GameSettingsManager.Apply();
    }

    private void SetSlider() => fpsSlider.SetActive(Res.data.gameSettingsData.fpsType == FpsType.Custom);
}
