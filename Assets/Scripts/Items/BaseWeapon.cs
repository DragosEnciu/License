using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : BaseStateItem {

	//BaseWeapon <- BaseStatItem <- BaseItem
    public enum WeaponType {AXE,STAFF,DAGGER}
    private int damage;

    private WeaponType typeWeapon;

        public WeaponType TypeWeapons {
        get { return typeWeapon; }
        set{ typeWeapon = value; }
        }
    public int WeaponDamage {
        get { return damage; }
        set { damage = value; }
    }
}
