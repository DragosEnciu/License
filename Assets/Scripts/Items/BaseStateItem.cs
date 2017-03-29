using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateItem : BaseItem
{ //stats
    private int stamina;
    private int endurance;
    private int strength;
    private int intellect;


    public int ItemStamina
    {
        get { return stamina; }
        set { stamina = value; }
    }
    public int ItemEndurance
    {
        get { return endurance; }
        set { endurance = value; }
    }
    public int ItemStrength
    {
        get { return strength; }
        set { strength = value; }
    }
    public int ItemIntellect
    {
        get { return intellect; }
        set { intellect = value; }
    }

}

