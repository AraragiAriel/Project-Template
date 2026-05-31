using UnityEngine;

public class Variation
{
    public float previous = 0f;
    public float current = 0f;
    public float tried = 0f;
    public float max = 0f;
    public bool setup = false;
    
    public float change => current - previous;    
    public Type type => GetType(change);
    public AppearanceType apperance => GetAppearance(current);

    public float triedChange => tried - previous;  
    public Type triedType => GetType(triedChange);
    public AppearanceType triedApperance => GetAppearance(tried);

    public float percentage => current/max;
    public bool damaged => !setup && triedType == Type.Decrease;

    private Type GetType(float change)
    {
        if(setup || Mathf.Approximately(change, 0f))
            return Type.None;
        if(change > 0f)
            return Type.Increase;
        return Type.Decrease;
    }

    private AppearanceType GetAppearance(float current)
    {
        if(setup)
            return AppearanceType.None;
        if(previous <= 0f){
            if(Mathf.Approximately(current, 0f))
                return AppearanceType.None;
            if(current > 0f)
                return AppearanceType.Appeared;
        } else {
            if(current <= 0f)
                return AppearanceType.Disappeared;
            if(current > 0f)
                return AppearanceType.Kept;
        }
        return AppearanceType.None;
    }

    public enum Type
    {
        None,
        Increase,
        Decrease,
    }

    public enum AppearanceType
    {
        None,
        Appeared,
        Disappeared,
        Kept,
    }
}
