using UnityEngine;
using System;
using UnityEngine.Serialization;

public abstract class EnumData: UIDAsset, ILocalizer, IColor 
{
    public abstract Enum Value{get;}

    public ColorTag color;
    [SerializeField] private LocalizedString _enumName;
    public virtual string enumName =>
        wrapName ? _enumName.Localize().ColorWrap(color) : _enumName.Localize();
    public LocalizedString enumDescription;
    public bool wrapName = true;

    public virtual ColorTag GetColor() => color;

    public virtual LocalizedString Localize(string field = default)
        => field switch
            {
                "name" => enumName,
                "description" => enumDescription,
                _ => enumName,
            };
}

public abstract class EnumData<T> : EnumData where T : Enum
{
    [SerializeField] private T _enum;
    public override Enum Value => _enum;
    public T type => _enum;
}