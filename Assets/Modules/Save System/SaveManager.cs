using System.Collections;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance {get; private set;}

    private void Awake()
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

        Res.data.currentSave.saveContainer.Load();
    }

    public void Save()
    {
        if(SceneChanger.currentScene == SceneType.Menu)
            return;

        StartCoroutine(SaveCo());
    }

    public void ChangeSave(SaveDataContainer save)
    {
        Res.data.currentSave.saveContainer = save;
        Res.data.currentSave.saveContainer.Load();
    }

    private IEnumerator SaveCo()
    {
        yield return new WaitForEndOfFrame();
        Res.data.currentSave.saveContainer.Save();
    }
}
