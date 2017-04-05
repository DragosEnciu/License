using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Potion", order = 32003)]
public class Consumable : ScriptableObject {

    public float ManaRestored;
    public float HpRestored;

}
