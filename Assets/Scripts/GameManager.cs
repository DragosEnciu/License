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
    public Vector3 ExplorationPosition;
    public Vector3 Nord;
    public Vector3 Sud;
    public Vector3 Est;
    public Vector3 West;
    public Text Message;
    public GameObject PrefabTextBox;
    public GameObject Conflict;
    public string ConflictName;
    public bool isWin;
    public string LoadedScene;
    public EventStartExploration.Position cardinalPosition;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        Sud = new Vector3(0, -0.5f, -23);
        Nord = new Vector3(0, -0.5f, 21);
        West = new Vector3(19, -0.5f, 0);
        Est = new Vector3(-19, -0.5f, 0);
        //ExplorationPosition = new Vector3(0, -0.5f, 23);
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        EventManager.Instance.AddListenerOnce<EventCombat>(EventCombat_Handler);
        EventManager.Instance.AddListenerOnce<EventStartExploration>(EventExploration_Hendler);
      
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
        LoadedScene = SceneManager.GetActiveScene().name;
        ExplorationPosition = CharacterInScene.transform.position;
        Application.LoadLevel("Combat");

        List<GameObject> SceneCharacters = ((EventCombat)e).Characters;
        ConflictName = ((EventCombat)e).Conflict;
        //Spawn(SceneCharacters);
        //TO DO 
        gameState = GameState.GameState_Combat;
    }
    public void CombatStatus()
    {
        if (GameObject.FindGameObjectWithTag("Hero") == null)
        {
            Message.text = "You Lose";
            Message.GetComponent<Animator>().Play("Idle", -1, 0f);
            isWin = false;
            EventManager.Instance.TriggerEvent(new EventStartExploration(LoadedScene, EventStartExploration.Position.S));
            
        }
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Message.text = "You Win";
            Message.GetComponent<Animator>().Play("Idle", -1, 0f);
            isWin = true;
            EventManager.Instance.TriggerEvent(new EventStartExploration(LoadedScene, EventStartExploration.Position.Special));
        }

    }
    public void EventExploration_Hendler(GameEvent e)
    {
        cardinalPosition = ((EventStartExploration)e).pos;
        Application.LoadLevel(((EventStartExploration)e).LoadedScene);
        gameState = GameState.GameState_Exploration;
        
    }
    private void OnLevelFinishedLoading(Scene a, LoadSceneMode lsm)
    {
        if (a.name == "Exploration1" || a.name == "Exploration2" || a.name == "Exploration3" || a.name == "Exploration4")
        {
            if (isWin == false)
            {
                ExplorationPosition = GetPosition(cardinalPosition);
            }
            else
            {
                Conflict = GameObject.Find(ConflictName);
                Conflict.SetActive(false);
                isWin = false; 
            }
            CharacterInScene = GameObject.FindGameObjectWithTag("Hero");
            CharacterInScene.transform.position = ExplorationPosition;

        }
        if(a.name == "Combat")
        {
            Message = Instantiate(PrefabTextBox).GetComponentInChildren<Text>();
            Message.text = "";
        }
    }
    public Vector3 GetPosition(EventStartExploration.Position a)
    {
        switch (a)
        {
            case EventStartExploration.Position.N :
                return Nord;
            case EventStartExploration.Position.S:
                return Sud;
            case EventStartExploration.Position.E:
                return Est;
            case EventStartExploration.Position.W:
                return West;
            case EventStartExploration.Position.Special:
                return ExplorationPosition;
        }
        return new Vector3(0, 0, 0);
    }
}