using System;
using UnityEngine;

public class UpgradeExe : MonoBehaviour
{
    private Upgrade upgrade;

    private void Awake(){
        try {
            upgrade = GetComponentInParent<Upgrade>();
        } catch (Exception ex) {
            Util.Debug("parent upgrade not found: " + ex.Message);
        }
    }

    private void OnEnable(){
        upgrade.OnSetEffect += SetEffect;
    }

    private void OnDisable(){
        upgrade.OnSetEffect -= SetEffect;        
    }

    protected virtual void SetEffect(int level){}
}
