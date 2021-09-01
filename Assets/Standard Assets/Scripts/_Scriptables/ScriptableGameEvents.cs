using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventProfile", menuName = "ScriptableObjects/GameEventProfile", order = 2)]

public class ScriptableGameEvents : ScriptableBase
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
    public enum InputEventType  
    {
     MapDrag,
     DisplacementDrag
    }

    [Serializable]
    public class TurnEventSettings // Each palette
    {
        public string Name;
        public TurnPhase turnPhase;
        public bool ReturnToCentre;
        public string TriggerEliasProfiler;
        public int CameraMode;
       }
    [Serializable]
    public class InputEventSettings // Each palette
    {
        public string Name;
        public InputEventType InputType;
    }

    public InputEventSettings[] InputEvents;    // array of all palettes

    public TurnEventSettings[] TurnEvents;    // array of all palettes
  
}
