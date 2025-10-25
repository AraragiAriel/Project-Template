using UnityEngine;
using System.Collections.Generic;

public class StatsManager : MonoBehaviour
{
    private RID id = new();

    private static StatsManager instance;

    private void Awake(){
        if(instance != null){
            Destroy(gameObject);
            return;
        } else {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    
        Reset();
    }

    private void OnDestroy(){
        Reset();
    }

    public void Reset(){
        foreach(Stat stat in Res.data.statsData.stats)
            stat.Initialize();

        #if UNITY_EDITOR
        foreach(StatOffset statOffset in Res.data.offsetData.offsets){
            if(statOffset.stat == null || statOffset.offset == 0f)
                continue;
            statOffset.stat.SetModifier(new ValueMod(id, statOffset.offset, ValueMod.Type.Flat));
        }
        #endif
    }
}
