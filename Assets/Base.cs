using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    public List<Power> abilities;
    public Stats stats;
    public Items HeroItem;
    public Weapons HeroWeapon;

    //setting constant modifiers for the fucntion 
    public const float STAMINAMODIFIER = 10;
    public const float ENDURANCEMODIFIER = 15;
    public const float MAXACTIONPOINTS = 100;

	//added automatic by the class they are from 
    public string ClassName;
    public string ClassDescription;

    public float Threat;
    // calculable stats
    [SerializeField]
    private float HP;
    [SerializeField]
    private float MANA;
    [SerializeField]
    protected float currentActionPoints;

    public float damagemultiplier = 1;

    public List<CharacterAction> actionQueue = new List<CharacterAction>();

    private void Start()
    {
        HP = stats.Stamina * STAMINAMODIFIER;
        MANA = stats.Endurance * ENDURANCEMODIFIER;
    }

    //Getting the values for the unsettables
    public float get_Hp { get { return HP; } }
    public float get_Mana { get { return MANA; } }
    public float get_ActionPoints { get { return currentActionPoints; } }


	//setting values after formulas 
	public void TakeDamage ( float damage )
	{
		HP -= damage;
	}
	public void UseMana(float abilityCost)
	{
		MANA -= abilityCost;
	}


    /// <summary>
    ///After using a ability or getting damaged the action bar will decress using this function
    /// </summary>
    /// <param name="action">action should be 30 , 60 or 90 acording to GDD </param>

    public void TakeAction (float action)
    {
        currentActionPoints -= action;
    }

    public virtual void Update()
    {// incressing the action points for the action to take place 
        if (currentActionPoints <= MAXACTIONPOINTS)
            currentActionPoints += Time.deltaTime * stats.Speed / 3;

        foreach (CharacterAction action in actionQueue.ToArray())
        {
            if (action.finished)
                actionQueue.Remove(action);
        }
        if (actionQueue.Count > 0)
            actionQueue[0].Update(Time.deltaTime);
    }

    public void MoveCharacter(Vector3 position)
    {
        MovementAction move = new MovementAction(position, 10);
        move.owner = gameObject;
        actionQueue.Add(move);
    }
    public void DoDamage (Base target,float dmg)
    {
        AttackAction atack = new AttackAction(target, dmg * damagemultiplier);
        atack.owner = gameObject;
        actionQueue.Add(atack);
        AttackMade();
    }
    public void DoHeal(Base target, float hl)
    {
        HealAction heal = new HealAction(target, hl);
        heal.owner = gameObject;
        actionQueue.Add(heal);
    }
    public void AttackMade()
    {
        foreach(CharacterAction action in actionQueue)
        {
            var buff = action as BuffAction;
            if(buff != null)
            {
                buff.DecreaseDuration();
            }
        }
    }
}
