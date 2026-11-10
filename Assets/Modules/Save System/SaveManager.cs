using System.Collections;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    protected override void Setup()
    {
        base.Setup();

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
