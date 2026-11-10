using System;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour
{
    public enum ToolType
    {
        Test1,
        Test2,
        Test3,
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
            switch (type)
            {
                case ToolType.Test1:
                    Util.Debug("test 1");
                    break;
                case ToolType.Test2:
                    Util.Debug("test 2");
                    break;
                case ToolType.Test3:
                    Util.Debug("test 3");
                    break;
            }
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
