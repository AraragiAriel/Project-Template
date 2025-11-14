using UnityEngine;
using Steamworks;
using System.Data.Common;

public class SteamManager : MonoBehaviour
{
    public static SteamManager instance;
    
    private bool logged = false;

    private void Awake(){

        #if UNITY_EDITOR
        if(Res.data.editorPreferences.disableSteam){
            Destroy(gameObject);
            return;
        }
        #endif

        if(instance != null){
            Destroy(gameObject);
            return;
        } else {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        try{
            SteamClient.Init(StaticData.steamId);
            logged = true;
        } catch {
            Debug.LogWarning("couldnt initialize steam");
        }
    }

    private void OnDisable(){
        if(logged)
            SteamClient.Shutdown();
    }

    private void Update(){
        if(logged)
            SteamClient.RunCallbacks();
    }

    public bool CheckAchievement(string id){
        if(!logged)
            return false;
        var achievement = GetAchievement(id);
        return achievement.State;
    }

    public void TriggerAchievement(string id){
        if(!logged)
            return;
        var achievement = GetAchievement(id);
        achievement.Trigger();
    }

    public void ClearAchievement(string id){
        if(!logged)
            return;
        var achievement = GetAchievement(id);
        achievement.Clear();
    }

    private Steamworks.Data.Achievement GetAchievement(string id){
        return new Steamworks.Data.Achievement(id);
    }
}
