using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour {
    private string ItemName;
    private string ItemDescriprion;
    private int itemID;
    public enum ItemTypes {
    RELIC,
    WEAPON,
    SCROLL,
    POTION
    }
    private ItemTypes type;
    
    public string Name
    {
        get { return ItemName; }
        set { ItemName = value; }
    }
    public string Description
    {
        get { return ItemDescriprion; }
        set { ItemDescriprion = value; }
    }
    public int ID {
        get { return itemID; }
        set { itemID = value; }
    }
    public ItemTypes typeBase
    {
        get { return type; }
        set { type = value; }
    }	
	
}
