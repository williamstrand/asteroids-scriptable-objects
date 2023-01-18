using Asteroids;
using Variables;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

#if false

[CustomEditor(typeof(Asteroid))]
public class AsteroidEditor : Editor
{
    public VisualTreeAsset UXML;

    public override VisualElement CreateInspectorGUI()
    {
        return base.CreateInspectorGUI();
        // var root = new VisualElement();
        // UXML.CloneTree(root);
        // return root;
    }
}

#endif