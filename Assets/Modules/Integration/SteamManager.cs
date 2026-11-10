using UnityEngine;
using Steamworks;
using System.Data.Common;

public class SteamManager : Singleton<SteamManager>
{
#if !UNITY_WEBGL || UNITY_EDITOR
    [SerializeField] private UintData steamID;
    
    private bool logged = false;

#if UNITY_EDITOR
    protected override bool Skip() => Res.data.editorPreferences.disableSteam;
#endif

    protected override void Setup()
    {
        base.Setup();

        try
        {
            SteamClient.Init(steamID);
            logged = true;
        }
        catch
        {
            Debug.LogWarning("couldnt initialize steam");
        }
    }

    private void OnDisable()
    {
        if(logged)
            SteamClient.Shutdown();
    }

    private void Update()
    {
        if(logged)
            SteamClient.RunCallbacks();
    }

    public bool CheckAchievement(string id)
    {
        if(!logged)
            return false;

        var achievement = GetAchievement(id);
        return achievement.State;
    }

    public void TriggerAchievement(string id)
    {
        if(!logged)
            return;

        var achievement = GetAchievement(id);
        achievement.Trigger();
    }

    public void ClearAchievement(string id)
    {
        if(!logged)
            return;

        var achievement = GetAchievement(id);
        achievement.Clear();
    }

    private Steamworks.Data.Achievement GetAchievement(string id)
    {
        return new Steamworks.Data.Achievement(id);
    }
#endif
}
