using System;
using System.Collections.Generic;
using UnityEngine;

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
        try
        {
            // switch (type)
            // {
            // }
        }
        catch
        {
            Util.Debug("Couldn't use dev tool");
        }
    }



    [System.Serializable]
    public class Slot
    {
        public ToolType type;
        public KeyCode key = KeyCode.None;
    }
}
