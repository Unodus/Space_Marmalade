using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointObject 
{
    public enum NodeMode // Each point can have three states- whether it's empty, able to be selected, or has a ship on it
    {
        Empty,
        Selectable,
        Occupied
    }

    //public ScriptableGrid gridSettings;
    public Vector2 Pos;
    public GameObject p;
    public NodeMode Mode;
    ParticleSystem Component;
    public UnityAction PointUpdateEvent;
    public PointObject()
    {
        OnEnable();


        ScriptableScripts.ScriptableScript s = ScriptableExtensions.s.scriptable;

 
        EventDictionary.StartListening(s.GameEvents.GetEventByType(s.Enums.GetEnum(GlobalEnum.InputEvent, 2)).Name, PointUpdateEvent);
    }

    public void OnEnable()
    {
        PointUpdateEvent += PointUpdate;
    }
    public void OnDisable()
    {
        PointUpdateEvent -= PointUpdate;
    }



    public void Init( Vector2 Position, GameObject Style)
    {
        Pos = Position;

        p = Object.Instantiate(Style, GridManager.currentGrid.transform);
        p.name = "Node:"+Position;
        p.tag = "Point";

        ScriptableGrid gridSettings = ScriptableExtensions.s.scriptable.Grids;

        p.transform.position = gridSettings.SetPosition(new Vector2(Position.x, Position.y), true);

        p.TryGetComponent(out Component);
        ChangeNodeType(NodeMode.Empty);
    }
    public void DeInit()
    {
        OnDisable();
        Object.Destroy(p);
    }
    public void PointUpdate()
    {
        GridSettings myGrid = ScriptableExtensions.s.scriptable.Grids.GetGridSettings().settings;
        if (p == null) return;
        float MovementTime = Vector3.Distance(p.transform.position, ScriptableExtensions.s.scriptable.Grids.SetPosition(new Vector2(Pos.x,Pos.y), true)) * myGrid.TransitionSpeed * Time.deltaTime;
        Vector3 TargetPosition = Vector3.MoveTowards(p.transform.position, ScriptableExtensions.s.scriptable.Grids.SetPosition(new Vector2(Pos.x, Pos.y), true), MovementTime);
        p.transform.position = TargetPosition;
    }

    public void ChangeNodeType(NodeMode newMode) // Used to calculate whether a node on the map is selectable
    {
        Mode = newMode;

        if (Component == null) return;

        ParticleSystem.MainModule settings = Component.main;
        if (Mode  == NodeMode.Empty)
        {
            settings.startColor = new Color(1, 1, 1, 0.5f);
        }
        else if (Mode == NodeMode.Occupied)
        {
            settings.startColor = Color.red;
        }
        else if (Mode == NodeMode.Selectable)
        {
            settings.startColor = Color.white;
        }

    }


}
