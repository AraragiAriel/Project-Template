using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DevTools : MonoBehaviour
{
    public enum ToolType
    {
    }

    [Header("Tools")]
    public List<Slot> slots = new();

    private void Awake()
    {
        GetComponentInChildren<DevToolsUI>().Setup(slots);
    }

    private void Update()
    {
        foreach(var slot in slots)
            if(slot != null && slot.key != KeyCode.None && Input.GetKeyDown(slot.key))
                Use(slot.type);
    }

    public void Use(ToolType type)
    {
        // switch (type)
        // {  
        // }
    }

    [System.Serializable]
    public class Slot
    {
        public ToolType type;
        public KeyCode key = KeyCode.None;
    }
}
