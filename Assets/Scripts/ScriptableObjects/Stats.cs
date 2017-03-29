using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats Container", order = 100000)]
public class Stats : ScriptableObject {
    public float Stamina;
    public float Strength;
    public float Endurance;
    public float Intellect;
    public float Speed; 
}

