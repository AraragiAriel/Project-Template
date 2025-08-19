using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

public static class StaticActions
{
    public static Action<GameState> OnGameStateChange;
    public static Action<ControlScheme> OnControlSchemeChange;

    // CURRENCY
    public static Action<CurrencyAmount, CurrencyAmount> OnCurrencyChange; // new amount, added amount
    public static Action<UpgradeData> OnBuyUpgrade;

    // SCENE
    public static Action<SceneType> OnSceneBeginChange; // scene before
    public static Action<SceneType> OnSceneUnload; // scene before
    public static Action<SceneType> OnSceneChange; // scene after

    // OTHERS
    public static Action OnGameSettingsChange;
}

public class Variation{
    public float previous = 0f;
    public float current = 0f;
    public float tried = 0f;
    public float max = 0f;
    public bool setup = false;
    
    public float change => current - previous;    
    public VariationType type => GetType(change);
    public AppearanceType apperance => GetAppearance(current);

    public float triedChange => tried - previous;  
    public VariationType triedType => GetType(triedChange);
    public AppearanceType triedApperance => GetAppearance(tried);

    public float percentage => current/max;
    public bool damaged => !setup && triedType == VariationType.Decrease;

    private VariationType GetType(float change){
        if(setup || Mathf.Approximately(change, 0f))
            return VariationType.None;
        if(change > 0f)
            return VariationType.Increase;
        return VariationType.Decrease;
    }

    private AppearanceType GetAppearance(float current){
        if(setup)
            return AppearanceType.None;
        if(previous <= 0f){
            if(Mathf.Approximately(current, 0f))
                return AppearanceType.None;
            if(current > 0f)
                return AppearanceType.Appeared;
        } else {
            if(current <= 0f)
                return AppearanceType.Disappeared;
            if(current > 0f)
                return AppearanceType.Kept;
        }
        return AppearanceType.None;
    }

    public enum VariationType{
        None,
        Increase,
        Decrease,
    }

    public enum AppearanceType{
        None,
        Appeared,
        Disappeared,
        Kept,
    }
}
