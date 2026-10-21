using UnityEngine;

[System.Serializable]
public class UpgradeRequirement
{
    private enum Type
    {
        Max = 0,
        Half = 1,
        Single = 2,
        Custom = 3,
    }

    public UpgradeData upgrade;
    [SerializeField] private Type type;
    public int level;
    public bool display = true;

    public int levelReq
        => type switch
        {
                Type.Max => upgrade.maxLevel,
                Type.Half => upgrade.maxLevel / 2,
                Type.Single => 1,
                Type.Custom => level,
                _ => 0,
        };
}
