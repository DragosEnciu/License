using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAction
{
    public GameObject owner = null;
    public bool finished = false;
    public abstract void Update(float dt);
}

public class AttackAction : CharacterAction
{
    private Base target;
    private float damage;
    public AttackAction(Base tar, float dmg) { target = tar; damage = dmg; }
    public override void Update(float dt)
    {


        if (!target || !owner)
        {
            finished = true;
            return;
        }
        target.TakeDamage(damage);
        finished = true;
    }

}


public class BuffAction : CharacterAction
{
    private int remainingAttacks;
    private float damageBonus;
    public BuffAction(int duration, float bonus)
    {
        remainingAttacks = duration;
        damageBonus = bonus;
    }
    public override void Update(float dt)
    {
        if (remainingAttacks == 0)
        {
            finished = true;
            if (owner)
                owner.GetComponent<Base>().damagemultiplier -= damageBonus;
            return;
        }

    }
    public void DecreaseDuration() { remainingAttacks--; }
    public void ApplyEffect()
    {
        if (owner)
            owner.GetComponent<Base>().damagemultiplier += damageBonus;
    }

}

public class MovementAction : CharacterAction
{
    private Vector3 targetPosition;
    private float movementSpeed;
    public MovementAction(Vector3 pos, float speed) { targetPosition = pos; movementSpeed = speed; }
    public override void Update(float dt)
    {
        if (!owner)
        {
            finished = true;
            return;
        }
        owner.transform.position = Vector3.MoveTowards(owner.transform.position, targetPosition, movementSpeed * dt);
        if (owner.transform.position == targetPosition)
            finished = true;
    }
}

public class HealAction : CharacterAction
{
    private Base target;
    private float heal;
    public HealAction(Base tar, float hl) { target = tar; hl = heal; }
    public override void Update(float dt)
    {


        if (!target || !owner)
        {
            finished = true;
            return;
        }
        target.TakeDamage(-heal);
        finished = true;
    }

}
