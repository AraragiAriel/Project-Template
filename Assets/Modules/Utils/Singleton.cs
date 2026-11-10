using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T instance {get; protected set;}

    private void Awake()
    {
        if(Skip() || instance != null)
        {
            DestroySetup();
            Destroy(gameObject);
            return;
        }
        else
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            Setup();
            instance = (T)this;
        }
    }

    protected virtual bool Skip() => false;
    protected virtual void DestroySetup(){}
    protected virtual void Setup(){}
}
