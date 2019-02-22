using System;
using UnityEngine;

public static class UColor
{
    public static byte CurrentColor;
    public static int x = 0;
    public static int y = 0;

    #region Team Color
    public static Color32 DarkDefault = new Color32(0, 0, 0, 255);
    public static Color32 LightDefault = new Color32(0, 0, 0, 255);
    public static Color32 DarkGreen = new Color32(0, 0, 0, 255);
    public static Color32 LightGreen = new Color32(0, 0, 0, 255);
    public static Color32 DarkBlue = new Color32(0, 0, 0, 255);
    public static Color32 LightBlue = new Color32(0, 0, 0, 255);
    #endregion
    #region Palette
    public static Color32[] Palette = new [] {
        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),
        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),

        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),
        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),

        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),
        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),

        new Color32(255, 0, 0, 255), new Color32(255, 204, 0, 255), new Color32(102, 255, 0, 255), new Color32(0, 255, 102, 255), //Main line
        new Color32(0, 204, 255, 255), new Color32(0, 102, 255, 255), new Color32(102, 255, 0, 255), new Color32(255, 0, 204, 255),

        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),
        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),

        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),
        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),

        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),
        new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255), new Color32(0, 0, 0, 255),

        new Color32(32, 32, 32, 255), new Color32(64, 64, 64, 255), new Color32(96, 96, 96, 255), new Color32(128, 128, 128, 255), //Monochrome line
        new Color32(160, 160, 160, 255), new Color32(192, 192, 192, 255), new Color32(224, 224, 224, 255), new Color32(240, 240, 240, 255)
    };
    #endregion

    public static Color32 ByteToColor(byte value)
    {
        var color = new Color32();
        color.r = (byte)((value >> 5) * 32);
        color.g = (byte)((value >> 2) * 32);
        color.b = (byte)(value * 64);
        color.a = 255;
        return color;
    }

    public static byte UintToByte(uint value)
    {
        byte r = (byte)(value >> 16);
        byte g = (byte)(value >> 8);
        byte b = (byte)(value >> 0);
        return (byte)(((r / 32) << 5) + ((g / 32) << 2) + (b / 64));
    }

    public static void ByteToPosition()
    {
        y = CurrentColor % 32;
        x = (CurrentColor - y) / 32;
        y = 31 - y;
    }
}
