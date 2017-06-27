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

    public float Threat = 100;
    // calculable stats
    [SerializeField]
    private float HP;
    public float MaxHP;
    [SerializeField]
    private float MANA;
    public float MaxMANA;
    [SerializeField]
    protected float currentActionPoints;

    public float damagemultiplier = 1;

    public List<CharacterAction> actionQueue = new List<CharacterAction>();

    public virtual void Start()
    {
        SetMaxHP();
        SetMaxMANA();

        HP = MaxHP ;
        MANA = MaxMANA;
    }
    public void SetMaxHP()
    {
        MaxHP = Stamina * STAMINAMODIFIER;

    }
    public void SetMaxMANA()
    {
        MaxMANA = Endurance * ENDURANCEMODIFIER;

    }

    //Getting the values for the unsettables
    public float get_Hp { get { return HP; } }


    public float get_Mana { get { return MANA; } }
    public float get_ActionPoints { get { return currentActionPoints; } }

    public float Strength
    {
        get
        {
            float strength = stats.Strength;
            if (HeroItem != null)
                strength += HeroItem.Strength;
            if (HeroWeapon != null)
                strength += HeroWeapon.Strength;
            return strength;
        }
    }
    public float Stamina
    {
        get
        {
            float stamina = stats.Stamina;
            if(HeroItem != null)
                stamina += HeroItem.Stamina;
            if (HeroWeapon != null)
                stamina += HeroWeapon.Stamina;
            return stamina;
        }
    }
    public float Endurance
    {
        get
        {
            float endurance = stats.Endurance;
            if (HeroItem != null)
                endurance += HeroItem.Endurance;
            if (HeroWeapon != null)
                endurance += HeroWeapon.Endurance;
            return endurance;
        }
    }
    public float Intellect
    {
        get
        {
            float intellect = stats.Intellect;
            if (HeroItem != null)
                intellect += HeroItem.Intellect;
            if (HeroWeapon != null)
                intellect += HeroWeapon.Intellect;
            return intellect;
        }
    }
    public float Speed
    {
        get
        {
            float speed = stats.Speed;
            if (HeroItem != null)
                speed += HeroItem.Speed;
            if(HeroWeapon != null)
                speed += HeroWeapon.Speed;
            return speed;
        }
    }


  
    public void TakeDamage ( float damage )
	{
		HP -= damage;
        if (HP < 0)
            HP = 0;
        if (HP > MaxHP)
            HP = MaxHP;
	}
	public void UseMana(float abilityCost)
	{
		MANA -= abilityCost;
	}
    public void TakeAction (float action)
    {
        currentActionPoints -= action;
    }



    public virtual void Update()
    {// incressing the action points for the action to take place 
        if (currentActionPoints <= MAXACTIONPOINTS)
            currentActionPoints += Time.deltaTime * Speed / 3;

        foreach (CharacterAction action in actionQueue.ToArray())
        {
            if (action.finished)
                actionQueue.Remove(action);
        }
        if (actionQueue.Count > 0)
            actionQueue[0].Update(Time.deltaTime);
        if (HP <= 0)
            Destroy(gameObject);
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


    /// <summary>
    /// Setting the Maximum values for the HP and the Mana the character can have
    /// </summary>
   
}
