using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewWepon : MonoBehaviour {

    private BaseWeapon newWepon;

    void Start() {
        ChooseWeaponType();
        Debug.Log(newWepon.Name);
        Debug.Log(newWepon.Description);
        Debug.Log(newWepon.ID.ToString());
        Debug.Log(newWepon.TypeWeapons);
    }

    public void CreateWepon()
    {
        newWepon = new BaseWeapon();
        newWepon.Name = "W" + Random.Range(1, 100);
        newWepon.Description = "this is a new wepon";
        newWepon.ID = Random.Range(1, 100);
        newWepon.ItemStamina = Random.Range(1, 11);
        newWepon.ItemEndurance = Random.Range(1, 11);
        newWepon.ItemStrength = Random.Range(1, 11);
        newWepon.ItemIntellect = Random.Range(1, 11);
       // ChooseWeaponType();
        newWepon.WeaponDamage = Random.Range(1, 11);
        newWepon.TypeWeapons = BaseWeapon.WeaponType.AXE;
    }

        private void ChooseWeaponType()
    {
      

    }


    }