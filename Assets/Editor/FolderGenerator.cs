using UnityEngine;
using System.Collections;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 创建基本文件夹 Tools/创建基本文件夹
/// </summary>
public class FolderGenerator
{
#if UNITY_EDITOR
    [MenuItem("CC-自定义工具/创建基本文件夹")]
    public static void GenerateBasicFolder()
    {
        string path = Application.dataPath + "/"; //路径 

        Directory.CreateDirectory(path + "/Scripts");
        Directory.CreateDirectory(path + "/Scenes");
        Directory.CreateDirectory(path + "/Prefabs");
        Directory.CreateDirectory(path + "/Materials");
        Directory.CreateDirectory(path + "/Textures");
        Directory.CreateDirectory(path + "/Audio");
        Directory.CreateDirectory(path + "/Animations");
        Directory.CreateDirectory(path + "/Models");
        Directory.CreateDirectory(path + "/Editor");
        Directory.CreateDirectory(path + "/Resources");
        Directory.CreateDirectory(path + "/StreamingAssets");
        Directory.CreateDirectory(path + "/Plugins");
        Directory.CreateDirectory(path + "/UI");
        Directory.CreateDirectory(path + "/Shaders");
        Directory.CreateDirectory(path + "/Fonts");

        AssetDatabase.Refresh();

        Debug.Log("Folders Created");
    }
#endif
}
