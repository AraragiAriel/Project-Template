using System;
using UnityEngine;

public class DiscordManager : Singleton<DiscordManager>
{
#if !UNITY_WEBGL || UNITY_EDITOR    
    [SerializeField] private LongData discordID;
    private Discord.Discord discord;
    private bool logged = false;

#if UNITY_EDITOR
    protected override bool Skip() => Res.data.editorPreferences.disableDiscord;
#endif

    protected override void Setup()
    {
        base.Setup();

        try
        {
            discord = new Discord.Discord(discordID, (ulong)Discord.CreateFlags.NoRequireDiscord);
            logged = true;
        }
        catch(Exception e)
        {
            logged = false;
            Debug.LogWarning("couldn't log into Discord: " + e.Message);
        }
    }

    private void OnDisable()
    {
        try
        {
            if(logged)
                discord.Dispose();
        }
        catch(Exception e)
        {
            Debug.LogWarning("couldn't dispose Discord: " + e.Message);
        }
    }

    private void Start(){
        try
        {
            if(logged)
                SetActivity();
        }
        catch(Exception e)
        {
            Debug.LogWarning("couldn't set Discord activity: " + e.Message);
        }
    }

    private void Update(){
        try
        {
            if(logged)
                discord.RunCallbacks();
        }
        catch(Exception e)
        {
            Debug.LogWarning("Discord callback error: " + e.Message);
        }
    }

    private void SetActivity()
    {
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                State = "Playing",
            };
            activityManager.UpdateActivity(activity, null);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Discord activity error: " + e.Message);
        }
    }
#endif
}
