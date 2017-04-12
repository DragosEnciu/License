using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Implements the observer pattern.
/// </summary>
public class GameManager : Singleton<GameManager>{
  
    private enum GameState { GameState_StartMenu , GameState_Combat, GameState_Exploration, GameState_Pause};
    private GameState gameState;
    public GameObject PauseMenu;
    public GameObject CharacterInScene;
    public Vector3 ExplorationPosition = new Vector3( 0, 0, 0);
    public Text Message;
    public GameObject PrefabTextBox;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        EventManager.Instance.AddListener<EventCombat>(EventCombat_Handler);
        EventManager.Instance.AddListener<EventStartExploration>(EventExploration_Hendler);
    }
    public void Update()
    {
        switch(gameState)
        {
            case GameState.GameState_Exploration:
                {
                    
                    break;
                }
            case GameState.GameState_StartMenu:
            {
                    //TO DO Start Menu Scene 
                    // Load
                    //On press default position spawn in first scene
            

                    break;
            }
           
            case GameState.GameState_Combat:
                {
                    CombatStatus();

                    break;
                }
            case GameState.GameState_Pause:
                {
                    Instantiate(PauseMenu);
                    break;
                }
        }
    }
   public void EventCombat_Handler(GameEvent e)
    {

        Application.LoadLevel("Combat");
        List<GameObject> SceneCharacters = ((EventCombat)e).Characters;
        //Spawn(SceneCharacters);
        //TO DO 
        gameState = GameState.GameState_Combat;
    }
    public void CombatStatus()
    {
        
        Message = Instantiate(PrefabTextBox).GetComponentInChildren<Text>();
        
        if (GameObject.FindGameObjectWithTag("Hero") == null)
        {
            Message.text = "You Lose";
            Message.GetComponent<Animator>().Play("Idle", -1, 0f);
            EventManager.Instance.TriggerEvent(new EventStartExploration());
        }
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Message.text = "You Win";
            Message.GetComponent<Animator>().Play("Idle", -1, 0f);
            EventManager.Instance.TriggerEvent(new EventStartExploration());
        }

    }
    public void EventExploration_Hendler(GameEvent e)
    {
        
        Application.LoadLevel("Exploration");
        EventManager.Instance.TriggerEvent(new EventStartExploration());
        gameState = GameState.GameState_Exploration;
    }
}