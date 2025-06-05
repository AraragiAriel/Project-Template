using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    // INSTANCE
    [SerializeField] private List<Achievement> achievements = new();

    private void Awake(){
        if(instance != null){
            Destroy(gameObject);
            return;
        } else {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start(){
        foreach(Achievement achievement in achievements){
            if(ResourcesSystem.save.achievementSave.completed.Contains(achievement.id)){
                Trigger(achievement.id);
                continue;
            }
                
            Instantiate(achievement, transform);
        }
    }

    public void Trigger(string id){
        if(ResourcesSystem.data.demo)
            return;
            
        // Set to save
        if(!ResourcesSystem.save.achievementSave.completed.Contains(id))
            ResourcesSystem.save.achievementSave.completed.Add(id);

        // Set to Steam
        if(!ResourcesSystem.data.demo)
            if(SteamManager.instance != null){
                if(!SteamManager.instance.CheckAchievement(id))
                    SteamManager.instance.TriggerAchievement(id);
            }
    }
}
