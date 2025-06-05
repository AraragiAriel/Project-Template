using UnityEngine;

public class DemoActive : MonoBehaviour
{
    private void Start(){
        gameObject.SetActive(ResourcesSystem.data.demo);
    }
}
