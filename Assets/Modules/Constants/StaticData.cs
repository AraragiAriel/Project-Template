using System;
using UnityEngine;

public static class StaticData
{
    public static string projectFolder => Application.dataPath + "/..";
    public static string customFolder => projectFolder + "/My Folder";
}
