using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventStartMenu : GameEvent
{

}
public class EventCombat : GameEvent
{
    public List<GameObject> Characters = new List<GameObject>();
    public EventCombat(List<GameObject> CombatCharacters)
    {
        Characters = CombatCharacters;
    }
}
public class EventStartExploration : GameEvent
{

}
public class EventStopExploration : GameEvent
{

}
public class EventPause : GameEvent
{

}