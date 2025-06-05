using System;
using UnityEngine;

public static class StaticData
{
    public static string projectFolder = Application.dataPath + "/..";
    public static string customFolder = projectFolder + "/My Folder";

    public static uint steamId = 1234567; // UNSET
    public static string steamAppPage = ""; // UNSET
    public static string steamWebPage = ""; // UNSET
    public static long discordId = 1355948047355613438; // UNSET
}
