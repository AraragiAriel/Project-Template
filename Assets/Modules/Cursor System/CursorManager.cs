using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static CursorManager instance;

    [SerializeField] private Texture2D defaultSprite;
    [SerializeField] private Texture2D webSprite;

    void Awake()
    {
        if(instance != null)
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

    void Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if(defaultSprite != null)
            Cursor.SetCursor(defaultSprite, Vector2.zero, CursorMode.Auto);
#else
        if(webSprite != null)
            Cursor.SetCursor(webSprite, Vector2.zero, CursorMode.Auto);
#endif
    }
}
