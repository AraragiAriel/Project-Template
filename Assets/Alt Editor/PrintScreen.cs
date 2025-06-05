using UnityEngine;

public class PrintScreen : MonoBehaviour
{
    #if UNITY_EDITOR

    [SerializeField] private KeyCode key;

    private const string prefKey = "screenshot";
    private string folderPath => StaticData.customFolder + "/Graphical Assets/Screenshots";
    private int id{
        get => PlayerPrefs.GetInt(prefKey, 0);
        set => PlayerPrefs.SetInt(prefKey, value);
    }

    private void Update(){
        if(Input.GetKeyDown(key)){
            ScreenCapture.CaptureScreenshot(folderPath + "/z_Screenshot " + id + ".png");
            Debug.Log("Screenshot taken " + id);
            id++;
        }
    }

    #endif
}
