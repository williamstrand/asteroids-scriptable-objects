using Asteroids;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


[CustomEditor(typeof(Asteroid))]
public class AsteroidEditor : Editor
{
    public VisualTreeAsset UXML;

    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        UXML.CloneTree(root);
        return root;
    }
}
