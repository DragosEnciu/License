using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatTaker : MonoBehaviour
{

    public GameObject targetedEnemy;
    // Use this for initialization
    private GameObject character = null;
    private Base BaseCharacter = null;
    public Text HeroName;
    public Text Message;
    public Image HPbar;
    public Image APbar;
    public Image ManaBar;
    public Button Ability1;
    public Button Ability2;
    public Button Ability3;

    //void Start()
    //{
     
    //}


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
            foreach (RaycastHit hitObject in hit)
            {
                if (hitObject.transform.tag == "Hero")
                {
                    character = hitObject.transform.gameObject;
                    BaseCharacter = character.GetComponent<Base>();
                    break;
                }
               // character = null;
               // BaseCharacter = null;
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
            HeroName.text = BaseCharacter.ClassName;
            HPbar.fillAmount = BaseCharacter.get_Hp / (BaseCharacter.stats.Stamina * Base.STAMINAMODIFIER);
            APbar.fillAmount = BaseCharacter.get_ActionPoints / (Base.MAXACTIONPOINTS);
            ManaBar.fillAmount = BaseCharacter.get_Mana / (BaseCharacter.stats.Endurance * Base.ENDURANCEMODIFIER);
        }
        else
        {
            HeroName.text = "";
            HPbar.enabled = false;
            APbar.enabled = false;
            ManaBar.enabled = false;
        }
        Ability1.onClick.AddListener(() => CastAbility(BaseCharacter.abilities[0]));
        Ability2.onClick.AddListener(() => CastAbility(BaseCharacter.abilities[1]));
        Ability3.onClick.AddListener(() => CastAbility(BaseCharacter.abilities[2]));

    }


    void CastAbility(Power CastedPower)
    {
        if(CastedPower.NeededAP < BaseCharacter.get_Mana)
        {
            if (CastedPower.NeededAP < BaseCharacter.get_ActionPoints)
            {
                if (CastedPower.isAoe == false)
                {
                    Message.text = " ChooseTarget ";

                    RaycastHit[] hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    hit = Physics.RaycastAll(ray);
                    foreach (RaycastHit hitObject in hit)
                    {
                        if (hitObject.transform.tag == "Enemy")
                        {
                            targetedEnemy = hitObject.transform.gameObject;
                            CastedPower.Action(BaseCharacter, new Base[] { targetedEnemy.GetComponent<Base>() });
                            break;
                        }
                        // CastedPower.Action(BaseCharacter, TestEnemy);
                    }
                }
                else
                { // If it's AOE it should that all the targets automaticly 
                }
                  
}
            else
            {
                Message.text = "Not enough Mana";
            }
        }
        else
        {
            Message.text = "Not enough Action Points";
        }
    }
}