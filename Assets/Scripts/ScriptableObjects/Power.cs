using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Power", order = 100000)]
public class Power : ScriptableObject
{
    public float NeededAP;
    public float NeededMana;
    public float BaseDamage;
    public bool isAoe;
    public bool isFriendlyCast;
    public bool isMoving;
    public float STRDamageModifier;
    public float INTDamageModifier;



    // I need to modifiy the Base to generate automatic the threat at the start of the fight and after that 
    // i need to add Threat to the heal 
    public virtual void Action(Base caster, Base[] targets)
    {
        float damage = BaseDamage + caster.Strength * STRDamageModifier + caster.Intellect * INTDamageModifier;
        caster.UseMana(NeededMana);
        caster.TakeAction(NeededAP);
        if (isAoe)
        {
            if (isFriendlyCast)
            {
                caster.Threat = 150;
                foreach (Base i in targets)
                    caster.DoHeal(i, damage);
            }
            else
            {
                caster.Threat = 100;
                if (isMoving)
                {
                    caster.MoveCharacter(targets[0].transform.position);
                    foreach (Base i in targets)
                    {
                        caster.DoDamage(i, damage);
                    }
                    caster.MoveCharacter(caster.transform.position);
                }
                else
                    foreach (Base i in targets)
                    {
                        caster.DoDamage(i, damage);

                    }
            }
        }
        else
        {
            if (isFriendlyCast)
            {
                caster.Threat = 120;
                caster.DoHeal(targets[0], damage);
            }
            else
            {
                caster.Threat = 100;
                if (isMoving)
                {
                    caster.MoveCharacter(targets[0].transform.position);
                    caster.DoDamage(targets[0], damage);
                    caster.MoveCharacter(caster.transform.position);
                }
                else
                {
                    caster.DoDamage(targets[0], damage);
                }

            }
        }

    }

}