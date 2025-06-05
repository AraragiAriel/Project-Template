using UnityEngine;

public class UICameraRef : MonoBehaviour
{
    public static Camera uiCamera;

    private void Awake(){
        uiCamera = this.GetComponent<Camera>();
    }
}
