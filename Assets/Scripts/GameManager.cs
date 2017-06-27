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
    public EventStartExploration.Position cardinalPosition = EventStartExploration.Position.N;
    public Button ContinueButton;
    public GameObject PrefabCanvas;
    public GameObject PrefacConflictZone;
    public bool combat1 = false, combat2 = false, combat3 = false, combat4 = false;
    public string combatname;
    private GameObject InstancedHero;
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        InstancedHero = Instantiate(CharacterInScene);
        Sud = new Vector3(0, -0.5f, -23);
        Nord = new Vector3(0, -0.5f, 21);
        West = new Vector3(19, -0.5f, 0);
        Est = new Vector3(-19, -0.5f, 0);
        //ExplorationPosition = new Vector3(0, -0.5f, 23);
        Instantiate(PrefacConflictZone);
        PrefacConflictZone.transform.position = new Vector3(-3.5f, 0f, 10.3f);
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        EventManager.Instance.AddListenerOnce<EventCombat>(EventCombat_Handler);
        EventManager.Instance.AddListenerOnce<EventStartExploration>(EventExploration_Hendler);
      
    }
    public void Update()
    {
        
      
        switch (gameState)
        {
            case GameState.GameState_Exploration:
                {
                   
                    break;
                }
            case GameState.GameState_StartMenu:
            {
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
        ExplorationPosition = InstancedHero.transform.position;
        Application.LoadLevel("Combat");

        List<GameObject> SceneCharacters = ((EventCombat)e).Characters;
        ConflictName = ((EventCombat)e).Conflict;
        gameState = GameState.GameState_Combat;
    }
    public void CombatStatus()
    {
        if (GameObject.FindGameObjectWithTag("Hero") == null)
        {
            Message.text = "You Lose";
            Message.GetComponent<Animator>().Play("Idle", -1, 0f);
            isWin = false;
            ContinueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
            ContinueButton.GetComponent<Image>().enabled = true;
            ContinueButton.onClick.AddListener(LoadLevelLose);

        }
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Message.text = "You Win";
            Message.GetComponent<Animator>().Play("Idle", -1, 0f);
            isWin = true;
            ContinueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
            ContinueButton.GetComponent<Image>().enabled = true;
            ContinueButton.onClick.AddListener(LoadLevelWin);

        }
    }
    public void LoadLevelWin()
    {
        EventManager.Instance.TriggerEvent(new EventStartExploration(LoadedScene, EventStartExploration.Position.Special));
        setcombat();
    }
    void setcombat()
    {
        if (combatname == "combat1")
            combat1 = true;
        if (combatname == "combat2")
            combat2 = true;
        if (combatname == "combat3")
            combat3 = true;
        if (combatname == "combat4")
            combat4 = true;
    }
    public void LoadLevelLose()
    {
        EventManager.Instance.TriggerEvent(new EventStartExploration(LoadedScene, EventStartExploration.Position.S));

    }
    public void EventExploration_Hendler(GameEvent e)
    {
        cardinalPosition = ((EventStartExploration)e).pos;
        SceneManager.LoadScene(((EventStartExploration)e).LoadedScene);
        //Application.LoadLevel(((EventStartExploration)e).LoadedScene);
        gameState = GameState.GameState_Exploration;
        
    }
    private void OnLevelFinishedLoading(Scene a, LoadSceneMode lsm)
    {
        if (combat4 == true && a.name != "End")
            Application.LoadLevel("End");
        //Initialization
        if (a.name != "Combat")
            InstancedHero = Instantiate(CharacterInScene);
        Sud = new Vector3(0, -0.5f, -23);
        Nord = new Vector3(0, -0.5f, 21);
        West = new Vector3(19, -0.5f, 0);
        Est = new Vector3(-19, -0.5f, 0);
        //ExplorationPosition = new Vector3(0, -0.5f, 23);
        EventManager.Instance.AddListenerOnce<EventCombat>(EventCombat_Handler);
        EventManager.Instance.AddListenerOnce<EventStartExploration>(EventExploration_Hendler);
        ///////Finish initialization
        if (a.name == "Exploration1" || a.name == "Exploration2" || a.name == "Exploration3" || a.name == "Exploration4")
        {
            if (isWin == false)
            {
                ExplorationPosition = GetPosition(cardinalPosition);
            }
            else
            {
                //Conflict = GameObject.Find(ConflictName);
                //Conflict.SetActive(false);
                isWin = false; 
            }
            //CharacterInScene = GameObject.FindGameObjectWithTag("Hero");
            if (a.name != "Combat")
                InstancedHero.transform.position = ExplorationPosition;
            if(a.name == "Exploration1" && combat1 == false)
            {
                Instantiate(PrefacConflictZone);
                PrefacConflictZone.transform.position = new Vector3(-3.5f,0f,10.3f);
                combatname = "combat1";
            }
            if (a.name == "Exploration2" && combat2 == false)
            {
                Instantiate(PrefacConflictZone);
                PrefacConflictZone.transform.position = new Vector3(-3.5f, 0f, 10.3f);
                combatname = "combat2";
            }
            if (a.name == "Exploration3" && combat3 == false)
            {
                Instantiate(PrefacConflictZone);
                PrefacConflictZone.transform.position = new Vector3(-3.5f, 0f, 10.3f);
                combatname = "combat3";
            }
            if (a.name == "Exploration4" && combat4 == false)
            {
                Instantiate(PrefacConflictZone);
                PrefacConflictZone.transform.position = new Vector3(-3.5f, 0f, 10.3f);
                combatname = "combat4";
            }
        }
        if(a.name == "Combat")
        {
            Message = Instantiate(PrefabTextBox).GetComponentInChildren<Text>();
            Instantiate(PrefabCanvas);
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