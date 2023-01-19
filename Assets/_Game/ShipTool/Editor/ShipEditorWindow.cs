using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Variables;
using Ship;

public class ShipEditorWindow : EditorWindow
{
    const string SHIP_SETTINGS_PATH = "Assets/_Game/Components/Ship/Ship Settings.asset";
    const string UXML_PATH = "Assets/_Game/ShipTool/ShipEditor.uxml";

    VisualTreeAsset _uxml;

    ShipSettings _shipSettings;

    [MenuItem("Window/Ship Editor")]
    static void CreateMenu()
    {
        var window = GetWindow<ShipEditorWindow>();
        window.titleContent = new GUIContent("Ship Editor");
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

    }

    private void BindProperties()
    {
        _shipSettings = AssetDatabase.LoadAssetAtPath<ShipSettings>(SHIP_SETTINGS_PATH);
        SerializedObject so = new SerializedObject(_shipSettings);

        SerializedProperty sp = so.FindProperty(nameof(ShipSettings.ThrottlePower));
        rootVisualElement.Q<PropertyField>("Throttle").BindProperty(sp);

        sp = so.FindProperty(nameof(ShipSettings.RotationPower));
        rootVisualElement.Q<PropertyField>("Rotation").BindProperty(sp);

        sp = so.FindProperty(nameof(ShipSettings.LaserSpeed));
        rootVisualElement.Q<PropertyField>("LaserSpeed").BindProperty(sp);
    }

    private void OnSelectionChange()
    {
        if (Selection.activeGameObject == null) return;

        if (Selection.activeGameObject.tag != "Player") return;

        var player = Selection.activeGameObject.GetComponent<Engine>();
        var gun = Selection.activeGameObject.GetComponentInChildren<Gun>();
        rootVisualElement.Q<Button>("Shoot").clicked += gun.Shoot;
        rootVisualElement.Q<Button>("SteerLeft").clicked += player.SteerLeft;
        rootVisualElement.Q<Button>("SteerRight").clicked += player.SteerRight;
        rootVisualElement.Q<Button>("ThrottleButton").clicked += player.Throttle;
    }
}
