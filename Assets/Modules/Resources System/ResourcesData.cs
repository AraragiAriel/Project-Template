using System.Collections.Generic;
using UnityEngine;

public class ResourcesData : ScriptableObject
{
    [Header("Options")]
    public bool demo;

    [Header("Data")]
    public CurrentSave currentSave;
    public GameSettingsData gameSettingsData;
    public List<GameSetting> gameSettings = new();
    public ScenePersistentData scenePersistentData;
    public CurrenciesData currenciesData;
    public StatsData statsData;
    public UpgradesData upgradesData;
    public UIDs uids;
    public ColorTagsData colorTags;
    public LocalizationData localizationData;
    public TmpAnimationsData tmpAnimationsData;

    [Header("Prefabs")]
    public AudioSourceManager audioSourcePrefab;
    public GameObject spriteShadow;
    public AfterEffect afterEffect;
    public Popup popup;
    public GameSettingsScreen gameSettingsScreen;

    [Header("UI")]
    public ConfirmationBox confirmationBox;

    [Header("Tooltips")]

#if UNITY_EDITOR
    [Header("Editor")]
    public EditorPreferencesData editorPreferences;
    public StatsOffsetData offsetData;
#endif
}
