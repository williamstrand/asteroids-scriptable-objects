using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Variables;

public class AsteroidEditorWindow : EditorWindow
{
    const string SHIP_SETTINGS_PATH = "Assets/_Game/Components/Asteroid/AsteroidSettings.asset";
    AsteroidSettings _asteroidSettings;
    const string UXML_PATH = "Assets/_Game/AsteroidTool/AsteroidEditor.uxml";


    VisualTreeAsset _uxml;

    [MenuItem("Window/Asteroid Editor")]
    static void CreateMenu()
    {
        var window = GetWindow<AsteroidEditorWindow>();
        window.titleContent = new GUIContent("Asteroid Editor");
    }

    public void CreateGUI()
    {
        LoadTree();

        BindProperties();

    }

    void LoadTree()
    {
        _uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH);
        _uxml.CloneTree(rootVisualElement);
    }

    void BindProperties()
    {
        _asteroidSettings = AssetDatabase.LoadAssetAtPath<AsteroidSettings>(SHIP_SETTINGS_PATH);
        SerializedObject so = new SerializedObject(_asteroidSettings);

        SerializedProperty sp = so.FindProperty(nameof(AsteroidSettings.MinForce));
        rootVisualElement.Q<Slider>("MinForce").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.MaxForce));
        rootVisualElement.Q<Slider>("MaxForce").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.MinSize));
        rootVisualElement.Q<Slider>("MinSize").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.MaxSize));
        rootVisualElement.Q<Slider>("MaxSize").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.MinTorque));
        rootVisualElement.Q<Slider>("MinTorque").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.MaxTorque));
        rootVisualElement.Q<Slider>("MaxTorque").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.Damage));
        rootVisualElement.Q<PropertyField>("Damage").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.Colors));
        rootVisualElement.Q<PropertyField>("Colors").BindProperty(sp);
    }
}
