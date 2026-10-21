using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Odin.OdinSerializer;
using UnityEngine;

public static class Formatter
{
    [System.Serializable]
    public class Options
    {
        public bool setDigits = false;
        public int digits = 6;
        public bool percent = false;
        public int decimalDigits = 2;
        public bool round = true;

        public bool minusSign = true;
        public bool plusSign = false;
        public bool spacedSign = false;
        public string postSign = "";

        public MetricPrefix prefix = MetricPrefix.Prefix;
        public int prefixDecimalDigits = 2;
        public int minE = 3;

        public bool includeSecondaryValue = false;
        public float secondaryValue = 0f;

        public enum MetricPrefix
        {
            None,
            ScientificNotation,
            Prefix,
        }

        public static Dictionary<int, string> metricPrefixes = new()
        {
            // { 30, "Q"},
            // { 27, "R"},
            // { 24, "Y"},
            // { 21, "Z"},
            // { 18, "E"},
            // { 15, "P"},
            // { 12, "T"},
            // {  9, "G"},
            {  6, "M"},
            {  3, "k"},
            // {  0, ""},
            // { -3, "m"},
            // { -6, "μ"},
            // { -9, "n"},
            // {-12, "p"},
            // {-15, "f"},
            // {-18, "a"},
            // {-21, "z"},
            // {-24, "y"},
            // {-27, "r"},
            // {-30, "q"},
        };

        public Options(){}
    }

    public static string Format(this float value) => value.Format(new());
    public static string Format(this int value) => value.Format(new());
    public static string Format(this int value, Options options) => ((float)value).Format(options);

    public static string Format(this float value, Options options)
    {
        options = SerializationUtility.CreateCopy(options) as Options;

        string sign = 
            value >= 0f ?
                options.plusSign ?
                    "+" + (options.spacedSign ? " " : "") :
                    ""  :
                options.minusSign ?
                    "-" + (options.spacedSign ? " " : "") :
                    ""  ;
        sign += options.postSign;

        value = Mathf.Abs(value);

        if (options.percent)
        {
            value *= 100;
        }

        int power = Mathf.Approximately(value, 0f) ? 0 : Mathf.FloorToInt(Mathf.Log10(value));

        // SET DIGITS
        if(options.setDigits && !Mathf.Approximately(value, 0f))
        {
            float scale = Mathf.Pow(10, options.digits - 1 - power);
            if(options.round)
                value = Mathf.Round(value*scale)/scale;
            else
                value = Mathf.Floor(value*scale)/scale;
        }

        // SET PREFIX
        string prefix = "";
        if (options.prefix != Options.MetricPrefix.None)
        {
            if (options.prefix == Options.MetricPrefix.Prefix)
            {
                int surplus = power.Mod(3);
                var foundNotation = Options.metricPrefixes.TryGetValue(power - surplus, out string notation);
                if (!foundNotation)
                {
                    options.prefix = Options.MetricPrefix.ScientificNotation;
                }
                else
                {
                    value *= Mathf.Pow(10, surplus - power);
                    prefix += notation;
                    options.decimalDigits = options.prefixDecimalDigits;
                }
            }
            if (options.prefix == Options.MetricPrefix.ScientificNotation && Mathf.Abs(power) >= options.minE)
            {
                value /= Mathf.Pow(10, power);
                prefix += $"e{power}";
                options.decimalDigits = options.prefixDecimalDigits;
            }
        }

        // SET DECIMAL
        float mult = Mathf.Pow(10, options.decimalDigits);
        value *= mult;
        if(options.round)
            value = Mathf.Round(value);
        else
            value = Mathf.Floor(value);
        value /= mult;

        string s = "";
        s += sign;
        s += value.ToString("0.##", CultureInfo.InvariantCulture);
        s += prefix;
        if(options.percent)
            s += "%";

        if(options.includeSecondaryValue)
        {
            options.includeSecondaryValue = false;
            options.plusSign = false;
            options.minusSign = false;
            options.postSign = "";
            s += $" [{options.secondaryValue.Format(options)}]".TagWrap("myColor=faded");
        }

        return s;
    }

    public static float SetDigits(float value, int digits)
    {
        if(value == 0)
            return 0f;

        int power = Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(value)));
        float scale = Mathf.Pow(10, digits - 1 - power);
        return Mathf.Round(value*scale)/scale;
    }
}