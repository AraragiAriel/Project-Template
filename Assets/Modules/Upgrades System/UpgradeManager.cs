using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    private Dictionary<UpgradeData, Upgrade> exes = new();

    protected override void Setup()
    {
        base.Setup();

        Res.save.upgradeSave.ClearRemoved();
        Res.save.upgradeSave.ConcatLimits();

        Transform t = transform;
        foreach(UpgradeData data in Res.data.upgradesData.upgrades)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(t);
            Upgrade newUpgrade = obj.AddComponent<Upgrade>();
            newUpgrade.data = data;
            exes.Add(data, newUpgrade);

            // Check for exe
            if(data.exe != null)
            {
                GameObject exe = Instantiate(data.exe, obj.transform);
            }
        }
    }

    public bool TryToBuy(UpgradeData data)
    {
        if(GetState(data) != UpgradeState.Available)
            return false;

        if(!CurrencyManager.instance.SpendCurrency(data.cost))
            return false;

        Res.save.upgradeSave.AddLevel(data.uid);
        StaticActions.OnBuyUpgrade?.Invoke(data);

        return true;
    }

    public Upgrade GetExe(UpgradeData data)
    {
        try
        {
            return exes[data];
        }
        catch
        {
            return null;
        }
    }

    public UpgradeState GetState(UpgradeData data) => GetExe(data).state;
}
