using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class GameEditor : EditorWindow
{
    const string UXML_PATH = "Assets/_Game/GameEditor/Game Editor.uxml";
    const string SHIP_UXML_PATH = "Assets/_Game/GameEditor/Ship Editor.uxml";
    const string ASTEROID_UXML_PATH = "Assets/_Game/GameEditor/Asteroid Editor.uxml";
    const string EMPTY_UXML_PATH = "Assets/_Game/GameEditor/Empty Editor.uxml";

    const string SHIP_SETTINGS_PATH = "Assets/_Game/Components/Ship/Ship Settings.asset";
    const string ASTEROID_SETTINGS_PATH = "Assets/_Game/Components/Asteroid/AsteroidSettings.asset";

    VisualElement _root;
    Label _currentEditorName;

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
        LoadTree(rootVisualElement, UXML_PATH);

        _root = rootVisualElement.Q<VisualElement>("Root");
        _currentEditorName = rootVisualElement.Q<Label>("Name");

        OnSelectionChange();
    }

    /// <summary>
    /// Loads the UXML and clones it to the root.
    /// </summary>
    /// <param name="root">the root visual element.</param>
    /// <param name="path">the path to the UXML.</param>
    private void LoadTree(VisualElement root, string path)
    {
        var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        uxml.CloneTree(root);
    }

    private void OnSelectionChange()
    {
        if (Selection.activeGameObject == null) return;

        // Remove all children of _root.
        _root.Clear();

        switch (Selection.activeGameObject.tag)
        {
            case "Player":
                LoadShipEditor();
                return;

            case "Asteroid":
                LoadAsteroidEditor();
                break;

            default:
                LoadEmptyEditor();
                break;
        }
    }

    /// <summary>
    /// Shows the empty editor.
    /// </summary>
    private void LoadEmptyEditor()
    {
        _currentEditorName.text = "Game Editor";

        LoadTree(_root, EMPTY_UXML_PATH);
    }

    /// <summary>
    /// Shows the Ship editor.
    /// </summary>
    private void LoadShipEditor()
    {
        _currentEditorName.text = "Ship Editor";

        LoadTree(_root, SHIP_UXML_PATH);

        // Load ship settings.
        _shipSettings = AssetDatabase.LoadAssetAtPath<ShipSettings>(SHIP_SETTINGS_PATH);
        SerializedObject so = new SerializedObject(_shipSettings);

        _root.Bind(so);
    }

    /// <summary>
    /// Shows the Asteroid editor.
    /// </summary>
    private void LoadAsteroidEditor()
    {
        _currentEditorName.text = "Asteroid Editor";

        LoadTree(_root, ASTEROID_UXML_PATH);

        // Load asteroid settings.
        _asteroidSettings = AssetDatabase.LoadAssetAtPath<AsteroidSettings>(ASTEROID_SETTINGS_PATH);
        SerializedObject so = new SerializedObject(_asteroidSettings);

        _root.Bind(so);
    }
}
