using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public static TimeScaleManager instance;

    [SerializeField] private bool resetOnSceneChange;

    private List<Unit> units = new();
    private RID freezeID = new();

    private void Awake()
    {
        if(instance != null){
            Destroy(gameObject);
            return;
        } else {
            transform.parent = null;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        StaticActions.OnSceneChange += SceneChange;
    }

    private void OnDisable()
    {
        StaticActions.OnSceneChange -= SceneChange;        
    }

    private void Start()
    {
        Clear();
    }

    public void AddUnit(Unit newUnit)
    {
        var unit = instance.units.Find(u => u.id == newUnit.id);
        if(unit != null)
            unit.scale = newUnit.scale;
        else
            instance.units.Add(newUnit);
        CalculateScale();
    }


    public void RemoveUnit(RID id)
    {
        instance.units.RemoveAll(u => u.id == id);
        CalculateScale();
    }

    private void CalculateScale()
    {
        float scale = 1f;
        foreach(Unit unit in instance.units)
            if(unit.scale < scale)
                scale = unit.scale;
        Time.timeScale = scale;
    }

    public void Freeze(int times)
    {
        if(times == 0)
            return;
            
        StartCoroutine(FreezeCo(times));
    }

    private void Clear()
    {
        units.Clear();
        CalculateScale();
    }

    private void SceneChange(SceneType scene)
    {
        if(resetOnSceneChange)
            Clear();
    }

    private IEnumerator FreezeCo(int times)
    {
        AddUnit(new Unit(freezeID, 0f));
        yield return new WaitForSecondsRealtime(times/60f);
        RemoveUnit(freezeID);
    }

    public class Unit
    {
        public RID id;
        public float scale;

        public Unit(RID id, float scale)
        {
            this.id = id;
            this.scale = scale;
        }
    }
}