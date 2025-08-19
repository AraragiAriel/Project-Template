using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesData", menuName = "ScriptableObject/Others/ResourcesData")]
public class ResourcesData : ScriptableObject
{
    [Header("Options")]
    public bool demo;

    [Space(20)]

    public CurrentSave currentSave;
    public GameSettingsData gameSettings;
    public AudioSourceManager audioSourcePrefab;
    public ScenePersistentData scenePersistentData;
    public AfterEffect afterEffect;
    public GameObject spriteShadow;
    public List<CurrencyData> currenciesData = new();
    public Popup popup;
    public ConfirmationBox confirmationBox;
    public StatsData statsData;
    public UpgradesData upgradesData;

    #if UNITY_EDITOR
    [Header("Editor")]
    public EditorPreferencesData editorPreferences;
    public StatsOffsetData offsetData;
    #endif
}
