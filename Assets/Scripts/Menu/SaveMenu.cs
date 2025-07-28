using UnityEngine;
using TMPro;

public class SaveMenu : MonoBehaviour
{
    [SerializeField] private SaveSO save;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject createButton;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void OnEnable(){
        UpdateMenu();
    }

    public void CreateSave(){
        save.Create();
        // UpdateMenu();
        Play();
    }

    public void Play(){
        Res.data.currentSave.saveSO = save;
        SceneChanger.instance.ChangeScene(SceneType.Main);
    }

    public void Delete(){
        ConfirmationBox.OpenBox(new ConfirmationBoxParameters(Res.String("Delete Save Box"), ConfirmDelete));
    }

    public void ConfirmDelete(){
        ConfirmationBox.OpenBox(new ConfirmationBoxParameters(Res.String("Really Delete Save Box"), ReallyConfirm));
    }

    public void ReallyConfirm(){
        save.Delete();
        UpdateMenu();
    }

    private void UpdateMenu(){
        if(!save.exists){
            SetButtons(false);
            descriptionText.text = "";
        } else {
            save.Load();
            SetButtons(true);
            descriptionText.text = save.saveName.ToUpperInvariant();
        }
    }

    private void SetButtons(bool exists){
        createButton.SetActive(!exists);
        playButton.SetActive(exists);
        deleteButton.SetActive(exists);
    }
}
