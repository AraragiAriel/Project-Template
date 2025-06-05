using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

public static class StaticActions
{
    public static Action<GameState> OnGameStateChange;

    // CURRENCY
    public static Action<CurrencyAmount, CurrencyAmount> OnCurrencyChange; // new amount, added amount

    // SCENE
    public static Action<SceneType, float> OnSceneBeginChange; // scene before, duration
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

    private VariationType GetType(float change){
        if(change > 0f)
            return VariationType.Increase;
        if(change < 0f)
            return VariationType.Decrease;
        return VariationType.None;
    }

    private AppearanceType GetAppearance(float current){
        if(previous <= 0f){
            if(current == 0f)
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
