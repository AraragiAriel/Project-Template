using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeScaleSource{
    ChallengeEnd = 0,
}

public struct TimeScaleUnit{
    public TimeScaleSource source;
    public float scale;

    public TimeScaleUnit(TimeScaleSource source, float scale){
        this.source = source;
        this.scale = scale;
    }
}

public class TimeScaleManager : MonoBehaviour
{
    // STATIC

    private static TimeScaleManager instance;

    public static void AddUnit(TimeScaleUnit newUnit){
        foreach(TimeScaleUnit unit in instance.units)
            if(unit.source == newUnit.source)
                return;

        instance.units.Add(newUnit);
        ApplyTimeScale();
    }


    public static void RemoveUnit(TimeScaleSource source){
        foreach(TimeScaleUnit unit in instance.units)
            if(unit.source == source){
                instance.units.Remove(unit);
                break;
            }

        ApplyTimeScale();
    }

    private static void ApplyTimeScale(){
        Time.timeScale = CalculateScale();
    }

    private static float CalculateScale(){
        float scale = 1f;
        foreach(TimeScaleUnit unit in instance.units)
            if(unit.scale < scale)
                scale = unit.scale;

        return scale;
    }

    public static void HitLag(int times = 1){
        instance.StartHitLag(times);
    }

    // INSTANCE

    private List<TimeScaleUnit> units = new();
    [SerializeField] private bool resetOnSceneChange;

    private void Awake(){
        if(instance != null){
            Destroy(gameObject);
            return;
        } else {
            transform.parent = null;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable(){
        StaticActions.OnSceneChange += SceneChange;
    }

    private void OnDisable(){
        StaticActions.OnSceneChange -= SceneChange;        
    }

    private void Start(){
        units.Clear();
    }

    private void SceneChange(SceneType scene){
        if(resetOnSceneChange)
            units.Clear();
    }

    private void StartHitLag(int times){
        StartCoroutine(HitLagCo(times));
    }

    private IEnumerator HitLagCo(int times){
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(times/60f);
        ApplyTimeScale();
    }
}
