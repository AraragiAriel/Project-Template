using UnityEngine;
using UnityEngine.UI;
using System;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private GameObject image;
    public Action<bool> OnToggle;
    
    private bool _toggle = false;
    private bool toggle{
        get => _toggle;
        set{
            _toggle = value;
            image.SetActive(_toggle);
            OnToggle?.Invoke(_toggle);
        }
    }

    public void Set(bool toggle){
        this.toggle = toggle;
    }

    public void Click(){
        toggle = !toggle;
    }

}
