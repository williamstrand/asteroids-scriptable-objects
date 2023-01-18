using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Settings/AsteroidSettings")]
public class AsteroidSettings : ScriptableObject
{
    [Range(0, 10)] public float MinForce;
    [Range(0, 10)] public float MaxForce;
    [Range(0, 10)] public float MinSize;
    [Range(0, 10)] public float MaxSize;
    [Range(0, 10)] public float MinTorque;
    [Range(0, 10)] public float MaxTorque;

    [Range(0, 10)] public int Damage;
    public Color[] Colors;
}
