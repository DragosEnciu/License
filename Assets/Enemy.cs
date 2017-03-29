using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Here appears i have a error and i don't know if i can inheritance multiple clases
// so i made base inheritance the MonoBehavior class 
public class Enemy : Base {
    public GameObject target;
    public float abilityCooldown;  

    public Enemy()
    {    }

   void Start()
    {
       
    }
    //Made both functions virtual to be able to modifie it later if needed
    public override void Update()
    {
        base.Update();
        if(currentActionPoints > 30 )
             DoMeeleAttack(50);
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
