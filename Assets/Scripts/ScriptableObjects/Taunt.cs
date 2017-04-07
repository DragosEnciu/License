using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Special Power/Taunt", order = 100001)]
public class Taunt : Power {
    public float threatModifier;
    public override void Action(Base caster, Base[] targets)
    {
        caster.TakeAction(NeededAP);
        caster.UseMana(NeededMana);
        caster.Threat += threatModifier;
    }

}
