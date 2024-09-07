using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SceneSwitcherWindow : EditorWindow
{
    // 定义一个键，它是一个字符串，用于标识你的偏好设置
    private const string PrefKeyCloseWindowAfterSwitch = "SceneSwitcher_CloseWindowAfterSwitch";

    private Dictionary<string, string> _scenesInBuild;
    private bool _closeWindowAfterSwitch = false;
    private Vector2 _scrollPosition;

    [MenuItem("CC-自定义工具/场景切换器 &q")]
    private static void ShowWindow()
    {
        GetWindow<SceneSwitcherWindow>("场景切换器");
    }

    private void OnEnable()
    {
        RefreshSceneList();
        // 当窗口启用时，获取存储的偏好设置值
        _closeWindowAfterSwitch = EditorPrefs.GetBool(PrefKeyCloseWindowAfterSwitch, false);
    }

    private void RefreshSceneList()
    {
        _scenesInBuild = new Dictionary<string, string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                string sceneName = Path.GetFileNameWithoutExtension(scene.path);
                _scenesInBuild[sceneName] = scene.path;
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("选择一个场景进行切换（场景需加入Build列表）", EditorStyles.boldLabel);

        // 当切换值时，存储新的偏好设置值
        bool newCloseWindowAfterSwitch = EditorGUILayout.Toggle("切换后关闭窗口", _closeWindowAfterSwitch);
        if (newCloseWindowAfterSwitch != _closeWindowAfterSwitch)
        {
            _closeWindowAfterSwitch = newCloseWindowAfterSwitch;
            EditorPrefs.SetBool(PrefKeyCloseWindowAfterSwitch, _closeWindowAfterSwitch);
        }

        if (GUILayout.Button("刷新"))
        {
            RefreshSceneList();
        }

        // 检查是否在运行游戏
        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.HelpBox("无法在运行时切换场景！", MessageType.Warning);
            if (GUILayout.Button("停止运行"))
            {
                EditorApplication.isPlaying = false;
            }
            GUI.enabled = false; // 如果正在运行游戏，则禁用GUI
        }

        GUILayout.BeginVertical("box");
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
        foreach (var scene in _scenesInBuild)
        {
            if (GUILayout.Button(scene.Key))
            {
                SwitchScene(scene.Value);
            }
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();

        // 如果GUI被禁用，重新启用它以确保后续的GUI元素可用
        GUI.enabled = true;
    }

    private void SwitchScene(string scenePath)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenePath);
            if (_closeWindowAfterSwitch)
            {
                Close();
            }
        }
    }
}