using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventStartMenu : GameEvent
{

}
public class EventCombat : GameEvent
{
    public List<GameObject> Characters = new List<GameObject>();
    public string Conflict;
    public EventCombat(List<GameObject> CombatCharacters, string ConflictName)
    {
        Characters = CombatCharacters;
        Conflict = ConflictName;
    }
}
public class EventStartExploration : GameEvent
{
    public string LoadedScene;
    public enum Position {N,S,E,W,Special}
    public Position pos;
    public EventStartExploration(string ExploredScene,Position a )
    {
        LoadedScene = ExploredScene;
        pos = a;
    }
}
public class EventStopExploration : GameEvent
{

}
public class EventPause : GameEvent
{

}