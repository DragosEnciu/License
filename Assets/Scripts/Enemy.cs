using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Here appears i have a error and i don't know if i can inheritance multiple clases
// so i made base inheritance the MonoBehavior class 
public class Enemy : Base {
    public GameObject target;
    public float abilityCooldown;
    private GameObject[] targets;
    public Enemy()
    {    }

   public override void Start()
    {
        base.Start();
        targets = GameObject.FindGameObjectsWithTag("Hero");    
    }
    //Made both functions virtual to be able to modifie it later if needed
    public override void Update()
    {

        base.Update();
        if (get_Hp > (MaxHP * 0.3))
        {if (currentActionPoints > 30)
            {
                ChooseTarget();
                DoMeeleAttack(25);
            }
        }
        else
        {
            if(currentActionPoints > 60)
            {
                DoAbility(100);
            }
        }
    }

    public void ChooseTarget()
    {
        float maxThreat = 0;
        target = null;
        foreach(GameObject obj in targets)
        {
            if (obj != null && obj.GetComponent<Base>().Threat >= maxThreat)
            {
                target = obj;
                maxThreat = obj.GetComponent<Base>().Threat;
            }
        }
    }

    public virtual void DoMeeleAttack(float damage)
    {
        if (!target)
            return;
        MoveCharacter(target.transform.position);
        TakeAction(30);
        DoDamage(target.GetComponent<Base>(),damage);
        MoveCharacter(transform.position);
             
    }

    ///<summary>
    ///A self heal ability for the enemy
    ///made a getter function for the HP and the Mana values in BASE script 
    /// </summary>
    public virtual void DoAbility(float damage)
    {
        TakeAction(60);
        DoDamage(this, -damage);
    }
}
