using System;

public static class StaticActions
{
    public static Action<GameState> OnGameStateChange;
    public static Action<ControlScheme> OnControlSchemeChange;

    // CURRENCY
    public static Action<CurrencyAmount, Variation> OnCurrencyChange; // new amount, added amount
    public static Action<UpgradeData> OnBuyUpgrade;
    public static Action OnEconUpdate; // used for money and prices change

    // SCENE
    public static Action<SceneType> OnSceneBeginChange; // scene before
    public static Action<SceneType> OnSceneUnload; // scene before
    public static Action<SceneType> OnSceneChange; // scene after

    // OTHERS
    public static Action OnGameSettingsChange;
}
