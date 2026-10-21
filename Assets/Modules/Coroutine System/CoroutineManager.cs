using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
            return;    
        }
    }
}
