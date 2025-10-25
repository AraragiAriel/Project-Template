using UnityEngine;

[System.Serializable]
public class Requirement{
    private enum RequirementType{
        Max = 0,
        Half = 1,
        Single = 2,
        Custom = 3,
    }

    public UpgradeData upgrade;
    [SerializeField] private RequirementType type;
    public int level;
    public bool display = true;

    public int levelReq{
        get{
            switch(type){
                case RequirementType.Max:
                    return upgrade.maxLevel;
                case RequirementType.Half:
                    return upgrade.maxLevel/2;
                case RequirementType.Single:
                    return 1;
                case RequirementType.Custom:
                    return level;
                default:
                    return 0;
            }
        }
    }
}
