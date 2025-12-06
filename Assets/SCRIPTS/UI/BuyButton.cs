using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class BuyButton : MonoBehaviour
{
    // public class Key{
    //     public int id;
    //     public bool active;
    // }
    // private List<Key> keys;

    public UnityEvent OnBuy;
    [SerializeField] private TextMeshProUGUI tmp;

    private Button _button;
    private Button button => _button??= GetComponent<Button>();

    private bool _canBuy;
    private bool canBuy{
        get => _canBuy;
        set{
            _canBuy = value;
            button.interactable = canBuy;
        }
    }

    private CurrencyAmount _price;
    private CurrencyAmount price{
        get => _price;
        set{
            _price = value;
            Evaluate();
        }
    }

    private bool _toggle = true;
    public bool toggle{
        get => _toggle;
        set{
            _toggle = value;
            Evaluate();
        }
    }

    private bool settedUp = false;

    private void OnEnable(){
        StaticActions.OnEconUpdate += EconUpdate;
    }

    private void OnDisable(){
        StaticActions.OnEconUpdate -= EconUpdate;        
    }

    public void SetPrice(CurrencyAmount newPrice){
        settedUp = true;
        price = newPrice;
        tmp.Set(price.Format());
    }

    public void Buy(){
        if(!CurrencyManager.instance.SpendCurrency(price)) return;

        OnBuy?.Invoke();
    }

    private void Evaluate(){
        if(!settedUp) return;

        canBuy = CurrencyManager.instance.HasEnoughCurrency(price) && toggle;
    }

    private void EconUpdate() => Evaluate();
}