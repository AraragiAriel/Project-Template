using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    private Dictionary<UpgradeData, Upgrade> exes = new();

    private void Awake(){
        if(instance != null){
            Destroy(gameObject);
            return;
        } else {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        Res.save.upgradeSave.ClearRemoved();
        Res.save.upgradeSave.ConcatLimits();

        Transform t = transform;
        foreach(UpgradeData data in Res.data.upgradesData.upgrades){
            GameObject obj = new GameObject();
            obj.transform.SetParent(t);
            Upgrade newUpgrade = obj.AddComponent<Upgrade>();
            newUpgrade.data = data;
            exes.Add(data, newUpgrade);

            // Check for exe
            if(data.exe != null){
                GameObject exe = Instantiate(data.exe, obj.transform);
            }
        }
    }

    public bool TryToBuy(UpgradeData data){
        if(GetState(data) != UpgradeState.Available)
            return false;

        if(!CurrencyManager.SpendCurrency(data.cost))
            return false;

        Res.save.upgradeSave.AddLevel(data.uid);
        StaticActions.OnBuyUpgrade?.Invoke(data);

        return true;
    }

    public Upgrade GetExe(UpgradeData data){
        try {
            return exes[data];
        } catch {
            return null;
        }
    }

    public UpgradeState GetState(UpgradeData data){
        return GetExe(data).state;
    }
}
