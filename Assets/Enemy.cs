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

   void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Hero");
    }
    //Made both functions virtual to be able to modifie it later if needed
    public override void Update()
    {
        base.Update();
        ChooseTarget();
        if(currentActionPoints > 30 )
             DoMeeleAttack(25);
    }

    public void ChooseTarget()
    {
        float maxThreat = 0;
        foreach(GameObject obj in targets)
        {     
            if (obj != null && obj.GetComponent<Base>().Threat > maxThreat)
                target = obj;
        }
    }

    ///<summary>
    /// temporary target chosen by the enemy until we figur out how to chose a enemy
    // if(target.hp < 30% * ) 
    ///  target.TakeDamage (damage)
    /// else 
    ///  randome chose a target
    ///</summary> 
    public virtual void DoMeeleAttack(float damage)
    {
        MoveCharacter(target.transform.position);
        TakeAction(30);
        DoDamage(target.GetComponent<Base>(),damage);
        MoveCharacter(transform.position);
             
    }

    ///<summary>
    ///A self heal ability for the enemy
    ///made a getter function for the HP and the Mana values in BASE script 
    /// </summary>
    public virtual void DoAbility(float damge)
    {
      

    }
}
