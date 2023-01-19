using Ship;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class GameEditor : EditorWindow
{
    const string UXML_PATH = "Assets/_Game/GameEditor/Game Editor.uxml";
    const string SHIP_UXML_PATH = "Assets/_Game/GameEditor/Ship Editor.uxml";
    const string ASTEROID_UXML_PATH = "Assets/_Game/GameEditor/Asteroid Editor.uxml"; 

    const string SHIP_SETTINGS_PATH = "Assets/_Game/Components/Ship/Ship Settings.asset";
    const string ASTEROID_SETTINGS_PATH = "Assets/_Game/Components/Asteroid/AsteroidSettings.asset";

    VisualElement _root;
    Label _currentEditorName;

    VisualTreeAsset _uxml;
    ShipSettings _shipSettings;
    AsteroidSettings _asteroidSettings;


    [MenuItem("Window/Game Editor")]
    static void CreateMenu()
    {
        var window = GetWindow<GameEditor>();
        window.titleContent = new GUIContent("Game Editor");
    }

    public void CreateGUI()
    {
        LoadTree();

        BindProperties();
    }

    private void LoadTree()
    {
        _uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_PATH);
        _uxml.CloneTree(rootVisualElement);

        _root = rootVisualElement.Q<VisualElement>("Root");
        _currentEditorName = rootVisualElement.Q<Label>("Name");
    }

    private void BindProperties()
    {
        
    }

    private void OnSelectionChange()
    {
        if (Selection.activeGameObject == null) return;


        switch (Selection.activeGameObject.tag)
        {
            case "Player":
                LoadShipEditor();
                return;

            case "Asteroid":
                LoadAsteroidEditor();
                break;

            default:
                break;
        }
    }

    private void LoadShipEditor()
    {
        _currentEditorName.text = "Ship Editor";
        _root.Clear();

        var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(SHIP_UXML_PATH);
        uxml.CloneTree(_root);

        _shipSettings = AssetDatabase.LoadAssetAtPath<ShipSettings>(SHIP_SETTINGS_PATH);
        SerializedObject so = new SerializedObject(_shipSettings);

        SerializedProperty sp = so.FindProperty(nameof(ShipSettings.ThrottlePower));
        _root.Q<PropertyField>("Throttle").BindProperty(sp);

        sp = so.FindProperty(nameof(ShipSettings.RotationPower));
        _root.Q<PropertyField>("Rotation").BindProperty(sp);

        sp = so.FindProperty(nameof(ShipSettings.LaserSpeed));
        _root.Q<PropertyField>("LaserSpeed").BindProperty(sp);
    }

    private void LoadAsteroidEditor()
    {
        _currentEditorName.text = "Asteroid Editor";
        _root.Clear();

        var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(ASTEROID_UXML_PATH);
        uxml.CloneTree(_root);

        _asteroidSettings = AssetDatabase.LoadAssetAtPath<AsteroidSettings>(ASTEROID_SETTINGS_PATH);
        SerializedObject so = new SerializedObject(_asteroidSettings);

        SerializedProperty sp = so.FindProperty(nameof(AsteroidSettings.MinForce));
        _root.Q<Slider>("MinForce").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.MaxForce));
        _root.Q<Slider>("MaxForce").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.MinSize));
        _root.Q<Slider>("MinSize").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.MaxSize));
        _root.Q<Slider>("MaxSize").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.MinTorque));
        _root.Q<Slider>("MinTorque").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.MaxTorque));
        _root.Q<Slider>("MaxTorque").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.Damage));
        _root.Q<PropertyField>("Damage").BindProperty(sp);

        sp = so.FindProperty(nameof(AsteroidSettings.Colors));
        _root.Q<PropertyField>("Colors").BindProperty(sp);
    }
}
