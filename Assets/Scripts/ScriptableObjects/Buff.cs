using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Special Power/Buff", order = 100001)]
public class Buff : Power {
    public int effectDuration;
    public float damageMultiplierBonus = 0.5f ; 
    public override void Action(Base caster, Base[] targets)
    {
        caster.TakeAction(NeededAP);
        caster.UseMana(NeededMana);
        foreach(Base i in targets)
        {
            BuffAction buff = new BuffAction(effectDuration, damageMultiplierBonus);
            buff.owner = i.gameObject;
            i.actionQueue.Add(buff);
            buff.ApplyEffect();
        }
    }
}
