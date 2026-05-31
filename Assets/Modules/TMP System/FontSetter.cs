using TMPro;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
[RequireComponent(typeof(TMP_Text))]
public class FontSetter : MonoBehaviour
{
    [SerializeField] private FontData data;

    private FontData registeredData;

    private TMP_Text _tmp;
    private TMP_Text tmp => _tmp ??= GetComponent<TMP_Text>();

    void Awake()
    {
        Set();
    }

    private void Set()
    {
        if(data == null || data.font == null || data.font == tmp.font)
            return;

        tmp.font = data.font;
#if UNITY_EDITOR
        EditorUtility.SetDirty(tmp);
#endif 
    }

#if UNITY_EDITOR
    void OnEnable()
    {
        if(data != null)
        {
            data.OnFontChanged += Set;
            registeredData = data;
        }
    }

    void OnDisable()
    {
        if(registeredData != null)
            data.OnFontChanged -= Set;
    }

    void OnValidate()
    {
        if(!gameObject.activeInHierarchy || !gameObject.scene.IsValid() || !gameObject.scene.isLoaded)
            return;

        if(registeredData != data)
        {
            if(registeredData != null)
                registeredData.OnFontChanged -= Set;
            registeredData = data;
            if(registeredData != null)
                registeredData.OnFontChanged += Set;
        }

        Set();
    }

    [MenuItem("Tools/AraragiAriel/Locate TMPs in scene missing Font Setter")]
    private static void FindMissingInScene()
    {
        var tmps = FindObjectsByType<TMP_Text>(FindObjectsSortMode.None).ToList();
        foreach(var tmp in tmps)
            if(!tmp.TryGetComponent(out FontSetter _))
                Debug.LogWarning("TMP missing Font Setter", tmp);
    }
#endif
}