using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <typeparam name="MyT">Configuartion's type, used for shortcuts</typeparam>
public abstract class Configuration<MyT> : Configuration where MyT : Configuration<MyT>, new()
{
    public static MyT Instance => Get<MyT>();
}

public abstract class Configuration : ScriptableObject
{
    // Static stuff
    public static Dictionary<Type, Configuration> ConfigurationLibrary = new Dictionary<Type, Configuration>();
    public static T Get<T>() where T : Configuration, new()
    {
        Type type = typeof(T);
        if (!ConfigurationLibrary.ContainsKey(type)) {
            // Gets configuration of type T if it's first time any object tries to get T
            var loadedConfigs = Resources.LoadAll<T>(@"_Configuration");
            if (loadedConfigs.Length > 0) {
                // Returns loaded configuration
                ConfigurationLibrary.Add(type, loadedConfigs[0]);
                return loadedConfigs[0];
            } else {
                // Creates new default configuration
                var defaultConfig = (T)CreateInstance(type);
                defaultConfig.name = $"[Temp] {ParseToNiceName(type.Name)}";
                ConfigurationLibrary.Add(type, defaultConfig);
                return defaultConfig;
            }
        } else {
            // Returns configuration from library of cached ones
            return (T)ConfigurationLibrary[type];
        }
    }

    public static Configuration GetFromResources(Type type)
    {
        var loadedConfigs = Resources.LoadAll(@"_Configuration", type);
        if (loadedConfigs.Length > 0) {
            return loadedConfigs[0] as Configuration;
        } else return null;
    }

    internal static string ParseToNiceName(string name)
    {
        name = Regex.Replace(name, @"<(\w+)>k__BackingField", @"$1");
        name = Regex.Replace(name, @"^[a-z]", m => m.ToString().ToUpper());
        name = Regex.Replace(name, @"([a-z])([A-Z])", @"$1 $2");
        name = Regex.Replace(name, @"([a-zA-Z])(\d)", @"$1 $2");
        name = Regex.Replace(name, @"(\d)([a-zA-Z])", @"$1 $2");
        return name;
    }
}

#if UNITY_EDITOR
public class ConfigurationWindow : EditorWindow
{
    private const string TempTag = "[Temp] ";

    private int selectedConfiguration;
    private Vector2 scrollPosition;
    private bool showEmpty = true;
    private List<Type> configurationTypes;
    bool dirty = true;

    [MenuItem("Window/Configuration")]
    static void Init()
    {
        ConfigurationWindow window = CreateInstance<ConfigurationWindow>();
        window.titleContent = new GUIContent("Configuration");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Select Configuration", EditorStyles.largeLabel);

        if (dirty || configurationTypes == null) {
            RecreateTypes();
        }

        selectedConfiguration = GUILayout.SelectionGrid(selectedConfiguration, configurationTypes.Select(c => GetTypeName(c)).ToArray(), 5);
        selectedConfiguration = Mathf.Clamp(selectedConfiguration, 0, configurationTypes.Count);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (configurationTypes.Count == 0) return;

        Configuration selectedConfig;
        Type type = configurationTypes[selectedConfiguration];
        if (Application.isPlaying) {
            if (Configuration.ConfigurationLibrary.ContainsKey(type)) {
                selectedConfig = Configuration.ConfigurationLibrary[type];
            } else {
                EditorGUILayout.HelpBox("This configuration was never used in game", MessageType.Warning);
                selectedConfig = Configuration.GetFromResources(type);
            }
        } else {
            selectedConfig = Configuration.GetFromResources(type);
        }
        if (selectedConfig != null) {
            if (selectedConfig.name.Contains(TempTag)) {
                // Is temporary
                EditorGUILayout.HelpBox("This configuration is temporary and will be destroyed after playmode is exited", MessageType.Warning);
                if (GUILayout.Button("Save to assets")) {
                    selectedConfig.name = selectedConfig.name.Replace(TempTag, "");
                    CreateAssetFrom(selectedConfig);
                }
                GUILayout.Label(selectedConfig.name.Replace(TempTag, ""), EditorStyles.largeLabel);
            } else {
                GUILayout.Label(selectedConfig.name, EditorStyles.largeLabel);
            }
            var editor = Editor.CreateEditor(selectedConfig);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            editor.OnInspectorGUI();
            GUILayout.EndScrollView();
        } else {
            EditorGUILayout.HelpBox("No configuration of this type was found", MessageType.Warning);
            if (GUILayout.Button("Create new")) {
                var configuration = CreateInstance(type);
                configuration.name = Configuration.ParseToNiceName(type.Name);
                CreateAssetFrom(configuration);
            }
        }
    }

    private void RecreateTypes()
    {
        configurationTypes = new List<Type>();
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
            foreach (var t in assembly.GetTypes()) {
                if (!t.IsAbstract && t.IsSubclassOf(typeof(Configuration))) {
                    configurationTypes.Add(t);
                }
            }
        }

        if (showEmpty) {
            configurationTypes = configurationTypes.Where(t => (Configuration.ConfigurationLibrary.ContainsKey(t) && Configuration.ConfigurationLibrary[t] != null) || Configuration.GetFromResources(t) != null).ToList();
        }

        dirty = false;
    }

    private void ShowButton(Rect rect)
    {
        rect.x += 4;
        rect.y += 4;
        bool last = showEmpty;
        showEmpty = !GUI.Toggle(rect, !showEmpty, GUIContent.none, "CircularToggle");
        if(showEmpty != last) {
            dirty = true;
        }
    }

    private static void CreateAssetFrom(ScriptableObject configuration)
    {
        string path = Path.Combine(Application.dataPath, "Resources", "_Configuration");
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
        AssetDatabase.CreateAsset(configuration, $"Assets/Resources/_Configuration/{configuration.name}.asset");
    }

    private string GetTypeName(Type type)
    {
        return Regex.Replace(type.Name, @"^(\w+)((_?Config)|(_?Configuration))$", @"$1");
    }
}
#endif