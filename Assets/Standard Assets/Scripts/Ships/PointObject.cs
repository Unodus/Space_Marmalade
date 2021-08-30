using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointObject 
{
    public enum NodeMode // Each point can have three states- whether it's empty, able to be selected, or has a ship on it
    {
        Empty,
        Selectable,
        Occupied
    }

    public ScriptableGrid gridSettings;
    public Vector2 Pos;
    public GameObject p;
    public NodeMode Mode;

    ParticleSystem Component;

    public void Init( Vector2 Position, GameObject Style)
    {
        Pos = Position;

        p = Object.Instantiate(Style, GridManager.currentGrid.transform);
        p.name = "Node:"+Position;
        p.tag = "Point";

        gridSettings = ScriptableExtensions.m_ScriptableGrid;

        p.transform.position = gridSettings.SetPosition(Position.x, Position.y);

        p.TryGetComponent(out Component);
        ChangeNodeType(NodeMode.Empty);
    }
    public void DeInit()
    {
        Object.Destroy(p);
    }
    public void PointUpdate()
    {
        ScriptableGrid.GridSettings myGrid = gridSettings.GetGridSettings();
        float MovementTime = Vector3.Distance(p.transform.position, gridSettings.SetPosition(Pos.x,Pos.y)) * myGrid.TransitionSpeed * Time.deltaTime;
        Vector3 TargetPosition = Vector3.MoveTowards(p.transform.position, gridSettings.SetPosition(Pos.x, Pos.y), MovementTime);
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
