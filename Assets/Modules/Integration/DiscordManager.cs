using System;
using UnityEngine;

public class DiscordManager : MonoBehaviour
{
    private static DiscordManager instance;
    
    private Discord.Discord discord;
    private bool logged = false;

    private void Awake(){

        #if UNITY_EDITOR
        if(Res.editor.disableDiscord){
            Destroy(gameObject);
            return;
        }
        #endif

        if(instance != null){
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        try{
            discord = new Discord.Discord(StaticData.discordId, (ulong)Discord.CreateFlags.NoRequireDiscord);
            logged = true;
        } catch(Exception e){
            logged = false;
            Debug.LogWarning("couldn't log into Discord: " + e.Message);
        }
    }

    private void OnDisable(){
        try{
            if(logged)
                discord.Dispose();
        } catch(Exception e){
            Debug.LogWarning("couldn't dispose Discord: " + e.Message);
        }
    }

    private void Start(){
        try{
            if(logged)
                SetActivity();
        } catch(Exception e){
            Debug.LogWarning("couldn't set Discord activity: " + e.Message);
        }
    }

    private void Update(){
        try{
            if(logged)
                discord.RunCallbacks();
        } catch(Exception e){
            Debug.LogWarning("Discord callback error: " + e.Message);
        }
    }

    private void SetActivity(){
        try{
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity{
                State = "Playing",
            };
            activityManager.UpdateActivity(activity, null);
        } catch (Exception e){
            Debug.LogWarning("Discord activity error: " + e.Message);
        }
    }
}
