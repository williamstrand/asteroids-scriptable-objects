using DefaultNamespace.ScriptableEvents;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Ship Settings")]
public class ShipSettings : ScriptableObject
{
    public float ThrottlePower;
    public float RotationPower;
}
