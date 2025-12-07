using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DevTools : MonoBehaviour
{
    [SerializeField] private DevToolsUI ui;

    [Header("Tools Parameters")]

    public List<DevTool> tools = new();

    KeyCode GetKey(int n) => (KeyCode)((int)KeyCode.Alpha0 + n);

    private void Awake(){
        DevTool test1 = new(){
            description = "test 1",
            action = () => {
                Util.Debug("test 1");
            }    
        };

        DevTool test2 = new(){
            description = "test 2",
            action = () => {
                Util.Debug("test 2");
            }    
        };

        DevTool test3 = new(){
            description = "test 3",
            action = () => {
                Util.Debug("test 3");
            }    
        };
        
        tools = new()
        {
            test1,
            test2,
            test3,
        };

        foreach(int i in 0.To(tools.Count - 1))
            if(tools[i] != null)
                tools[i].key = i <= 8 ? GetKey(i + 1) : KeyCode.None;

        ui.Setup(tools);
    }

    private void Update(){
        foreach(var tool in tools)
            if(tool != null && tool.key != KeyCode.None && Input.GetKeyDown(tool.key))
                tool.Call();
    }
}

public class DevTool{
    public string description;
    public Action action;
    public KeyCode key = KeyCode.None;

    public void Call(){
        try{
            action.Invoke();
        } catch {}
    }
}
