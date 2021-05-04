using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventProfile", menuName = "ScriptableObjects/GameEventProfile", order = 2)]

public class ScriptableGameEvents : ScriptableObject
{

    public enum TurnPhase // Turns are divided into different segments, to filter out user inputs.
    {
        EnemyTurnShooting,
        EnemyTurnMoving,
        PlayerTurn_Moving,
        PlayerTurn_Shooting,
        Transition,
        PlayerTurn_Start,
        PlayerTurn_End
    }


    [Serializable]
    public class EventSettings // Each palette
    {
        public string Name;
        public TurnPhase turnPhase;
        public bool ReturnToCentre;
        public string TriggerEliasProfiler;
       }

    public EventSettings[] Events;    // array of all palettes

    public EventSettings GetEventByName(string myName)
    {
        foreach (EventSettings i in Events)
        {
            if (i.Name == myName)
            {
                return i;
            }
        }

        Debug.LogWarning(myName + " is not registered in the profiler");
        return null;
    }
    public EventSettings GetEventByPhase(TurnPhase myEnum)
    {
        foreach (EventSettings i in Events)
        {
            if (i.turnPhase == myEnum)
            {
                return i;
            }
        }

        Debug.LogWarning(myEnum + " is not registered in the profiler");
        return null;
    }

}
