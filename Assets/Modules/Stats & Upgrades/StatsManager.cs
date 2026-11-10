using UnityEngine;

public class StatsManager : Singleton<StatsManager>
{
    private RID id = new();

    protected override void Setup()
    {
        base.Setup();

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
