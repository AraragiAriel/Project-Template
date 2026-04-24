using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // STATIC

    private static CameraManager _instance;
    public static CameraManager instance
    {
        get
        {
            if(_instance == null)
                _instance = FindAnyObjectByType<CameraManager>();
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    // INSTANCE

    [SerializeField] private List<Pair> cameras;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public Camera Get(Type type)
    {
        return cameras.Find(p => p.type == type).camera;
    }

    public enum Type
    {
        Default = 1,
        UI = 2,
    }

    void OnValidate()
    {
        foreach(var type in Util.EnumList<Type>())
        {
            if(!cameras.Exists(p => p.type == type))
            {
                cameras.Add(
                    new Pair
                    {
                        type = type,          
                    }
                );
            }
        }
    }

    [System.Serializable]
    private class Pair
    {
        public Type type;
        public Camera camera;
    }
}
