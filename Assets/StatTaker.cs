using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatTaker : MonoBehaviour
{

    public GameObject targetedEnemy = null;
    // Use this for initialization
    private GameObject character = null;
    private Base BaseCharacter = null;
    public Text HeroName;
    public Text Message;
    public Image HPbar;
    public Image APbar;
    public Image ManaBar;
    public Button[] Abilities;
    private bool isTargeting = false;
    private Power CurentAbility;

    List<Base> TargetableAllies = new List<Base>();


    void Start()
    {
        var heroes = GameObject.FindGameObjectsWithTag("Hero");
        for (int i=0; i<heroes.Length; i++)
        {   
            TargetableAllies.Add(heroes[i].GetComponent<Base>());
        }
    }
    void Update()
    {
        ///<summary>
        ///First i tried founding the object by using the RayCast
        /// </summary>

        if (Input.GetMouseButtonDown(0) == true && isTargeting == false)
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

                    for (int i = 0; i < Abilities.Length; i++)
                    {
                        Abilities[i].onClick.RemoveAllListeners();
                    }
                    for (int i = 0; i < Abilities.Length; i++)
                    {
                        Power ability = BaseCharacter.abilities[i];
                        Abilities[i].onClick.AddListener(delegate { CastAbility(ability); });
                    }
                    break;
                }
                // character = null;
                //  BaseCharacter = null;
            }
        }
        if (Input.GetMouseButtonDown(0) == true && isTargeting == true)
        {
            ChooseTarger();
            if (targetedEnemy)
            {

                Base[] a = new Base[] { targetedEnemy.GetComponent<Base>() };
                CurentAbility.Action(BaseCharacter, a);
                Message.text = "";
            }
            isTargeting = false;
        }


            ///<summary>
            ///Here i set the fill amount for each bar based on the stats of character gameObject 
            /// </summary>
        if (character != null)
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
    }

    void ChooseTarger()
    {

        {
            RaycastHit[] hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics.RaycastAll(ray);
            foreach (RaycastHit hitObject in hit)
            {
                if (hitObject.transform.tag == "Enemy" )
                {
                    targetedEnemy = hitObject.transform.gameObject;
                    break;
                }
            }
        }
    }

    void CastAbility(Power CastedPower)
    {
        if (CastedPower.GetType() == typeof(Taunt) || CastedPower.GetType() == typeof(Buff))
        {
            CastedPower.Action(BaseCharacter, TargetableAllies.ToArray());
            return;
        }
        if (CastedPower.NeededMana < BaseCharacter.get_Mana)
        {
            if (CastedPower.NeededAP < BaseCharacter.get_ActionPoints)
            {
                if (CastedPower.isAoe == false)
                {

                    Message.text = " ChooseTarget ";
                    Message.GetComponent<Animator>().Play("Idle", -1, 0f);
                    isTargeting = true;
                    CurentAbility = CastedPower;
                    // CastedPower.Action(BaseCharacter, targetedEnemy);
                }
            }
            else
            {
                Message.text = "Not enough Action Points";
                Message.GetComponent<Animator>().Play("FadeOut", -1, 0f);
            }
        }
        else
        {   
            Message.text = "Not enough Mana";
            Message.GetComponent<Animator>().Play("FadeOut", -1, 0f); 
        }
        //private delegate void skill1(){CastAbility(Power A)};
    }
}




    
