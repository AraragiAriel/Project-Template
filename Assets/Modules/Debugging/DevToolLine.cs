using UnityEngine;
using TMPro;

public class DevToolLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    private DevTool tool;

    public void Setup(DevTool tool){
        this.tool = tool;
        string s = "";
        if(tool.key != KeyCode.None)
            s += $"<size=8><b>[{tool.key - KeyCode.Alpha0}]</b></size> ";
        s += tool.description;
        tmp.Set(s);
    }

    public void Click(){
        tool.Call();
    }
}
