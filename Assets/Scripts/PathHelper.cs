using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathHelper
{
    public static readonly string ASSETS_PATH = "AssetsBundle";
    public static readonly string ASSETS_PATH_SCRIPTS = ASSETS_PATH + "/Scripts";
    public static string platformFile
    {
        get
        {
            string platform = "";
#if UNITY_WEBGL
            platform = "WebGL";
#elif UNITY_ANDROID
            platform = "Android";
#elif UNITY_IOS
            platform = "iOS";
#elif UNITY_STANDALONE_WIN
            platform = "StandaloneWindows";
#endif
            return platform;
        }
    }
    public static string GetLocalAssetsPath(bool forceAB = false)
    {
        string path = "";
#if UNITY_EDITOR
        path = string.Format("{0}/{1}", Application.dataPath, ASSETS_PATH);
#endif
        if (forceAB || string.IsNullOrEmpty(path))
        {
            path = string.Format("{0}/{1}", Application.persistentDataPath, platformFile);
        }
        return path;
    }
    public static string GetServerAssetsPath()
    {
        return "";
    }
}