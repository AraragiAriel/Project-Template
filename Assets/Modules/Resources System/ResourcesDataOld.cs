using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesDataOld", menuName = "ScriptableObject/Others/ResourcesDataOld")]
public class ResourcesDataOld : ScriptableObject
{
    [Header("Options")]
    public bool demo;

    [Header("Data")]
    public CurrentSave currentSave;
    public GameSettingsData gameSettings;
    public ScenePersistentData scenePersistentData;
    public CurrenciesData currenciesData;
    public StatsData statsData;
    public UpgradesData upgradesData;
    public UIDs uids;
    public ColorTagsData colorTags;
    public LocalizationData localizationData;

    [Header("Prefabs")]
    public AudioSourceManager audioSourcePrefab;
    public GameObject spriteShadow;
    public AfterEffect afterEffect;
    public Popup popup;

    [Header("UI")]
    public ConfirmationBox confirmationBox;

    [Header("Tooltips")]

    #if UNITY_EDITOR
    [Header("Editor")]
    public EditorPreferencesData editorPreferences;
    public StatsOffsetData offsetData;
    #endif
}
