using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Special Power/Stun", order = 100001)]
public class Stun : Power {

    /// <summary>
    /// The Stun consist of the allie charecter taking from the enemy character's AP bar and if
    /// it's less then the ActionPointsTaken the AP will have negative values 
    /// </summary>


    public float ActionPointsTaken;

    public virtual void Action(Base caster, Base[] targets)
    {
        caster.UseMana(NeededMana);
        caster.TakeAction(NeededAP);
        foreach (Base i in targets)
        {
            i.TakeAction(ActionPointsTaken);
        }
    }
    
}
