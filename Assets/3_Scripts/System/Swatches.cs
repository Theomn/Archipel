using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Swatch
{
    string name;
    Color color;
}

public class Swatches
{
    public const string green = "1dab6f";
    public const string red = "eb634b";
    public const string blue = "62b7d4";
    public const string yellow = "f5cc51";
    public const string darkBlue = "0c3e54";

    public static Color HexToColor(string hex)
    {
        float r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        return new Color(r,g,b,1);
    }
}
