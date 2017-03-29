using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Special Power/Taunt", order = 100001)]
public class Taunt : Power {

    public virtual void Action( Base caster)
    {
        caster.Threat = 160;
    }

}
