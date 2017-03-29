using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatTaker : MonoBehaviour {

    // Use this for initialization
    private GameObject character = null;
    public Text HeroName;
    public Image HPbar;
    public Image APbar;
    public Image ManaBar;
    

	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        ///<summary>
        ///First i tried founding the object by using the RayCast
        ///didn't worked as planned but tomorrow i will fix this
        /// </summary>
        if (Input.GetMouseButtonDown(0) == true)
        {
            RaycastHit[] hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics.RaycastAll(ray);
            foreach(RaycastHit hitObject in hit)
            {
                if (hitObject.transform.tag == "Hero")
                {
                    character = hitObject.transform.gameObject;
                    break;
                }
                character = null;
            }   
        }


        ///<summary>
        ///Here i set the fill amount for each bar based on the stats of character gameObject 
        /// </summary>
        if (character)
        {
            HPbar.enabled = true;
            APbar.enabled = true;
            ManaBar.enabled = true;
            HeroName.text = character.GetComponent<Base>().ClassName;
            HPbar.fillAmount = character.GetComponent<Base>().get_Hp / (character.GetComponent<Base>().stats.Stamina * Base.STAMINAMODIFIER);
            APbar.fillAmount = character.GetComponent<Base>().get_ActionPoints / (Base.MAXACTIONPOINTS);
            ManaBar.fillAmount = character.GetComponent<Base>().get_Mana / (character.GetComponent<Base>().stats.Endurance * Base.ENDURANCEMODIFIER);
        }
        else
        {
            HeroName.text = "";
            HPbar.enabled = false;
            APbar.enabled = false;
            ManaBar.enabled = false;
        }
       



    }
}
