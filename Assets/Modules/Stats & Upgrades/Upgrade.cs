using UnityEngine;
using System;
using System.Linq;

public enum UpgradeState{
    Null,
    Hidden,
    Unavailable,
    Available,
    Maxed,
}

public class Upgrade : MonoBehaviour
{
    public UpgradeData data;
    public Action<UpgradeState> OnStateChange;
    public Action<int> OnSetEffect;

    private bool _subbedToOnBuy = false;
    private bool subbedToOnBuy{
        set{
            if(value == _subbedToOnBuy) return;

            if(value)
                StaticActions.OnBuyUpgrade += OnBuy;
            else
                StaticActions.OnBuyUpgrade -= OnBuy;
            _subbedToOnBuy = value;
        }
    }

    private bool _subbedToOnCurrency = false;
    private bool subbedToOnCurrency{
        set{
            if(value == _subbedToOnCurrency) return;

            if(value)
                StaticActions.OnEconUpdate += EconUpdate;
            else
                StaticActions.OnEconUpdate -= EconUpdate;
            _subbedToOnCurrency = value;
        }
    }

    private UpgradeState _state = UpgradeState.Null;
    public UpgradeState state{
        get => _state;
        set{
            _state = value;
            OnStateChange?.Invoke(_state);
        }
    }

    private void OnDisable(){
        subbedToOnBuy = false;
        subbedToOnCurrency = false;
    }

    public void Start(){
        SetState();
        SetEffect();
    }

    private void OnBuy(UpgradeData data){
        SetState();

        if(data == this.data)
            SetEffect();
    }
    private void EconUpdate() => SetState();

    private void SetState(){
        if(data.maxedOut){
            state = UpgradeState.Maxed;
            subbedToOnBuy = false;
            subbedToOnCurrency = false;
        } else if(!data.MeetRequirements()){
            state = UpgradeState.Hidden;
            subbedToOnBuy = true;
            subbedToOnCurrency = false;
        } else if(CurrencyManager.instance.HasEnoughCurrency(data.cost)){
            state = UpgradeState.Available;
            subbedToOnBuy = true;
            subbedToOnCurrency = true;
        } else {
            state = UpgradeState.Unavailable;
            subbedToOnBuy = true;
            subbedToOnCurrency = true;
        }
    }

    protected virtual void SetEffect(){
        if(data.level <= 0)
            return;

        if(data.stat != null)
            data.stat.SetModifier(new ValueMod(data.rid, data.valuePerLevel*data.level, data.valueModType));
        OnSetEffect?.Invoke(data.level);
    }
}
