using UnityEngine;
using System.Collections.Generic;

public class StatsManager : MonoBehaviour
{
    private readonly string id = "statOffset";

    private static StatsManager instance;
    private bool initialized = false;

    private void Awake(){
        if(instance != null){
            Destroy(gameObject);
            return;
        } else {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            initialized = true;
        }
    
        foreach(Stat stat in Res.data.statsData.stats)
            stat.Initialize();

        #if UNITY_EDITOR
        foreach(StatOffset statOffset in Res.data.offsetData.offsets){
            if(statOffset.stat == null || statOffset.offset == 0f)
                continue;
            statOffset.stat.SetModifier(id, statOffset.offset, false);
        }
        #endif
    }

    private void OnDestroy() {
        if(initialized)
            foreach(Stat stat in Res.data.statsData.stats)
                stat.Initialize();
    }
}
