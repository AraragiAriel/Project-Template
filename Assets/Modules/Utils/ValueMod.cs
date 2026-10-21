using UnityEngine;

[System.Serializable]
public class ValueMod
{
    public enum Type
    {
        Flat,
        Percent,
        Mult,
    }

    [HideInInspector] public RID id;
    public float value;
    public Type type;

    public virtual string Format() => Format(new(this));

    public ValueMod(){}

    public ValueMod(ValueMod valueMod)
    {
        id = valueMod.id;
        value = valueMod.value;
        type = valueMod.type;
    }

    public ValueMod(RID id, float value, Type type)
    {
        this.id = id;
        this.value = value;
        this.type = type;
    }

    public static string Format(ValueFormatting formatting)
    {
        var typeToUse = formatting.valueMod.type;
        if(typeToUse == Type.Flat && formatting.overrideFlatToPercent)
            typeToUse = Type.Percent;
        if(formatting.onlyNonMultPlusSign)
            formatting.plusSign = formatting.valueMod.type != Type.Mult;

        switch(typeToUse)
        {
            case Type.Flat:
                return formatting.valueMod.value.Format(formatting);
            case Type.Percent:
                formatting.percent = true;
                return formatting.valueMod.value.Format(formatting);
            case Type.Mult:
                string toReturn = "";
                if(formatting.xPos == ValueFormatting.X_Position.First)
                    toReturn += "X";
                if(formatting.xPos == ValueFormatting.X_Position.Middle)
                    formatting.postSign = "X";
                toReturn += formatting.valueMod.value.Format(formatting);
                if(formatting.xPos == ValueFormatting.X_Position.End)
                    toReturn += "X";
                return toReturn;
            default:
                return formatting.valueMod.value.Format(formatting);
        } 
    }

    public class ValueFormatting : Formatter.Options
    {
        public enum X_Position
        {
            First = 0,
            Middle = 1,
            End = 2,
        }
        public ValueMod valueMod;
        public bool overrideFlatToPercent = false;
        public bool onlyNonMultPlusSign = false;
        public X_Position xPos = X_Position.Middle;

        public virtual string Format() => ValueMod.Format(this);

        public ValueFormatting(){}

        public ValueFormatting(ValueMod valueMod)
        {
            this.valueMod = new(valueMod);
        }
    }
}
